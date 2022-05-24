using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    internal class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetService(IAssetRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }
        public async Task<AssetDto> AddNewAsset(AssetDto userAsset)
        {
            var asset = await _assetRepository.Add(_mapper.Map<Asset>(userAsset));

            return _mapper.Map<AssetDto>(asset);
        }

        public async Task<PhotoDto> AddPhoto(AssetDto assetDto, PhotoDto photoDto)
        {

            var photo = await _assetRepository.AddPhoto(_mapper.Map<Asset>(assetDto), _mapper.Map<Photo>(photoDto));

            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<IEnumerable<AssetDto>> GetAllAssets()
        {
            var assets = await _assetRepository.GetAssets();

            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }

        public async Task<AssetDto> GetAssetById(int id)
        {
            var asset = await _assetRepository.GetAssetById(id);

            return _mapper.Map<AssetDto>(asset);
        }

        public PhotoDto MapPhotoToDto(Photo photo)
        {
            return _mapper.Map<PhotoDto>(photo);
        }
    }
}
