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
        private readonly IMapper _mapper;
        private readonly IPagedListRepository _pagedListRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssetService(IMapper mapper,
                            IPagedListRepository pagedListRepository,
                            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _pagedListRepository = pagedListRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<AddNewAssetDto> AddNewAsset(AddNewAssetDto userAsset)
        {
            //var option = new JsonSerializerOptions();
            //option.Converters.Add(new JsonStringEnumConverter());
            //var dataResponse = JsonSerializer.Deserialize<AddNewAssetDto>(userAsset, option);
            var asset = await _unitOfWork.AssetRepository.Add(_mapper.Map<Asset>(userAsset));

            return _mapper.Map<AddNewAssetDto>(asset);
        }

        public async Task<PhotoDto> AddPhoto(AssetDto assetDto, PhotoDto photoDto)
        {

            var asset = await _unitOfWork.AssetRepository.GetAssetById(assetDto.Id);
            asset.Photos.Add(_mapper.Map<Photo>(photoDto));

            return photoDto;
        }

        public async Task<IEnumerable<AssetDto>> GetAllAssets()
        {
            var assets = await _unitOfWork.AssetRepository.GetAssets();

            return _mapper.Map<IEnumerable<AssetDto>>(assets);
        }

        public async Task<AssetDto> GetAssetById(int id)
        {
            var asset = await _unitOfWork.AssetRepository.GetAssetById(id);

            return _mapper.Map<AssetDto>(asset);
        }

        public async Task<bool> UpdateAsset(UpdateAssetDto updateAssetDto)
        {
            var asset = await _unitOfWork.AssetRepository.GetAssetById2(updateAssetDto.Id);
            _mapper.Map(updateAssetDto, asset);

            _unitOfWork.AssetRepository.Update(asset);
            if (await _unitOfWork.AssetRepository.SaveAllAsync())
                return true;

            return false;
        }

        public async Task<IEnumerable<AssetDto>> GetUserAssets(int id)
        {
            var listOfAssets = await _unitOfWork.AssetRepository.GetUserAssets(id);

            return _mapper.Map<IEnumerable<AssetDto>>(listOfAssets);
        }

        public async Task<PagedList<AssetDto>> GetPagedAssets(AssetParams assetParams)
        {
            return await _pagedListRepository.GetPagedAssets(assetParams);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _unitOfWork.Complete();
        }
    }
}
