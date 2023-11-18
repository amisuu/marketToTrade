using Application;
using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    public class AssetsController : BaseController
    {
        private readonly IAssetService _assetService;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;

        public AssetsController(IAssetService assetService,
                                IPhotoService photoService,
                                IUserService userService)
        {
            _assetService = assetService;
            _photoService = photoService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssets([FromQuery] AssetParams assetParams)
        {
            var assets = await _assetService.GetPagedAssets(assetParams);

            Response.AddPaginationHeader(assets.CurrentPage, assets.PageSize, assets.TotalCount, assets.TotalPages);

            return Ok(await _assetService.GetAllAssets());
        }

        [HttpGet("{id}", Name = "GetAsset")]
        public async Task<AssetDto> GetAssetById(int id)
        {
            return await _assetService.GetAssetById(id);
        }

        [HttpPost("add-asset")]
        public async Task<AddNewAssetDto> AddNewAsset(AddNewAssetDto userAsset)
        {
            userAsset.AppUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var newAsset = await _assetService.AddNewAsset(userAsset);


            return newAsset;
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, [FromHeader] string id)
        {
            var idToInt = int.Parse(id);

            var asset = await _assetService.GetAssetById(idToInt);

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photoDto = await _photoService.GetNewPhotoResult(result, null, asset);
            //var photoDto = new PhotoDto
            //{
            //    Url = result.SecureUrl.AbsoluteUri,
            //    PublicId = result.PublicId,
            //};

            //await _assetService.AddPhoto(asset, photoDto);

            if (await _assetService.SaveAllAsync())
            {
                return photoDto;
                //return CreatedAtRoute("GetAsset", new {id = asset.Id}, _assetService.MapPhotoToDto(photo));
            }

            return BadRequest("Problem adding photo");
            // return photoDto;
        }

        [HttpPut("update-asset")]
        public async Task<ActionResult> UpdateAsset(UpdateAssetDto updateAssetDto)
        {
            if (await _assetService.UpdateAsset(updateAssetDto))
                return NoContent();

            return BadRequest("Failed to update advertisment");
        }

        [HttpGet("user-assets/{id}")]
        public async Task<IEnumerable<AssetDto>> GetUserAssets(int id)
        {
            var currentUser = User.FindFirst(ClaimTypes.SerialNumber)?.Value;

            var listOfAssets = await _assetService.GetUserAssets(id);

            return listOfAssets;
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId, int id)
        {
            var asset = await _assetService.GetAssetById(id);

            var photo = asset.Photos.FirstOrDefault(x => x.IsMain);
            if (photo.IsMain)
            {
                return BadRequest("This is your main photo");
            }

            var currentMain = asset.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null)
            {
                currentMain.IsMain = false;
                photo.IsMain = true;
            }

            return NoContent();
        }
    }
}
