using Application;
using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    public class LikesController : BaseController
    {
        private readonly ILikesService _likesService;
        private readonly IUserService _userService;
        private readonly IAssetService _assetService;

        public LikesController(ILikesService likesService, IUserService userService, IAssetService assetService)
        {
            _likesService = likesService;
            _userService = userService;
            _assetService = assetService;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> AddLike(int id)
        {
            var userId = User.GetUserId();
            var userAssets = await _assetService.GetUserAssets(userId);
            var likedAsset = await _assetService.GetAssetById(id);
            var sourceAsset = await _likesService.GetUserWithLikes(userId);

            if (likedAsset == null)
            {
                return NotFound();
            }

            foreach (var item in userAssets)
            {
                if (item.Id == likedAsset.Id)
                {
                    return BadRequest("You cannot like your items");
                }
            }

            var assetLike = await _likesService.GetAssetLike(userId, likedAsset.Id);

            if (assetLike != null)
            {
                return BadRequest("You already like this asset");
            }

            assetLike = new AssetLikeDto
            {
                SourceUserId = userId,
                LikedAssetId = likedAsset.Id,
            };
            //await sourceAsset.LikedAssets.Add(assetLike);
            await _likesService.AddLike(assetLike, userId);
            await _userService.SaveAllAsync();
            return Ok();
        }

        [HttpGet("assetLikes/{predicate}")]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetAssetLikes(string predicate)
        {
            var userId = User.GetUserId();
            var assets = await _likesService.GetAssetLikes(predicate, userId);

            //Response.AddPaginationHeader(assets.CurrentPage, assets.PageSize, assets.TotalCount, assets.TotalPages);

            return Ok(assets);
        }

        [HttpGet("usersWithLike/{predicate}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserWithLike(string predicate)
        {
            var likesParams = new LikesParams();
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);// User.GetUserId();// int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            likesParams.Predicate = predicate;
            var users = await _likesService.GetUserWithLike(predicate, userId);

            //Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }
    }
}
