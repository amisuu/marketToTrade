using Application;
using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public async Task<ActionResult> AddLike(int assetId)
        {
            var sourceAsset = await _assetService.GetAssetById(assetId);
            var likedAsset = await _likesService.GetAssetWitkLikes(sourceAsset.Id);

            if (likedAsset.Id == assetId)
            {
                return BadRequest("You cannot like your items");
            }
            var assetLike = await _likesService.GetAssetLike(sourceAsset.Id, likedAsset.Id);

            if (assetLike != null)
            {
                return BadRequest("You already like this asset");
            }

            assetLike = new AssetLikeDto
            {
                SourceAssetId = sourceAsset.Id,
                LikedAssetId = likedAsset.Id,
            };

            likedAsset.LikedAssets.Add(assetLike);
            await _userService.SaveAllAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetAssetLikes(string predicate, int assetId)
        {
            var asset = await _assetService.GetAssetById(assetId);
            var assets = await _likesService.GetAssetLikes(predicate, asset.Id);
            return Ok(assets);
        }
    }
}
