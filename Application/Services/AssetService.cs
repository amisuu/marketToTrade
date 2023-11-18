using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.Services
{
    internal class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;
        private readonly IPagedListRepository _pagedListRepository;

        public AssetService(IAssetRepository assetRepository,
                            IMapper mapper,
                            IPagedListRepository pagedListRepository)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
            _pagedListRepository = pagedListRepository;
        }
        public async Task<AddNewAssetDto> AddNewAsset(AddNewAssetDto userAsset)
        {
            //var option = new JsonSerializerOptions();
            //option.Converters.Add(new JsonStringEnumConverter());
            //var dataResponse = JsonSerializer.Deserialize<AddNewAssetDto>(userAsset, option);
            var asset = await _assetRepository.Add(_mapper.Map<Asset>(userAsset));

            return _mapper.Map<AddNewAssetDto>(asset);
        }

        public async Task<PhotoDto> AddPhoto(AssetDto assetDto, PhotoDto photoDto)
        {

            var asset = await _assetRepository.GetAssetById(assetDto.Id);
            asset.Photos.Add(_mapper.Map<Photo>(photoDto));

            return photoDto;
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

        public async Task<bool> UpdateAsset(UpdateAssetDto updateAssetDto)
        {
            var asset = await _assetRepository.GetAssetById2(updateAssetDto.Id);
            _mapper.Map(updateAssetDto, asset);

            _assetRepository.Update(asset);
            if (await _assetRepository.SaveAllAsync())
                return true;

            return false;
        }

        public async Task<IEnumerable<AssetDto>> GetUserAssets(int id)
        {
            var listOfAssets = await _assetRepository.GetUserAssets(id);

            return _mapper.Map<IEnumerable<AssetDto>>(listOfAssets);
        }

        public async Task<PagedList<AssetDto>> GetPagedAssets(AssetParams assetParams)
        {
            var assets = await _pagedListRepository.GetPagedAssets(assetParams);

            return assets;
        }

        public async Task<bool> SaveAllAsync()
        {
            if (await _assetRepository.SaveAllAsync())
                return true;

            return false;
        }
    }
}
