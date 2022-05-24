using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AssetsController : BaseController
    {
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public AssetsController(IAssetService assetService, 
                                IMapper mapper,
                                IPhotoService photoService)
        {
            _assetService = assetService;
            _mapper = mapper;
            _photoService = photoService;
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

        [HttpPost]
        public async Task<AssetDto> AddNewAsset(AssetDto userAsset)
        {
            var newAsset = await _assetService.AddNewAsset(userAsset);

            return _mapper.Map<AssetDto>(newAsset);
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
    }
}
