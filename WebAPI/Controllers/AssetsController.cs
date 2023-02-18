using Application;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    public class AssetsController : BaseController
    {
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;

        public AssetsController(IAssetService assetService, 
                                IMapper mapper,
                                IPhotoService photoService,
                                IUserService userService)
        {
            _assetService = assetService;
            _mapper = mapper;
            _photoService = photoService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssets()
        {
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
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int id)
        {
            var asset = await _assetService.GetAssetById(id);

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            if (await _assetService.AddPhoto(asset, _assetService.MapPhotoToDto(photo)) != null)
            {
                return _assetService.MapPhotoToDto(photo);
                //return CreatedAtRoute("GetAsset", new {id = asset.Id}, _assetService.MapPhotoToDto(photo));
            }

            return BadRequest("Problem adding photo");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsset(UpdateAssetDto assetDto)
        {
            var asset = await _assetService.GetAssetById(assetDto.Id);

            var mappedAsset = _assetService.MapAssetDtoToAsset(asset);

            await _assetService.UpdateAsset(assetDto);

            return NoContent();
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
