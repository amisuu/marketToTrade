using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAssetService
    {
        public Task<IEnumerable<AssetDto>> GetAllAssets();
        public Task<AssetDto> GetAssetById(int id);
        public Task<AddNewAssetDto> AddNewAsset(AddNewAssetDto user);
        public Task<PhotoDto> AddPhoto(AssetDto asset, PhotoDto photo);
        public PhotoDto MapPhotoToDto(Photo photo);
        public Asset MapAssetDtoToAsset(AssetDto assetDto);
        public Task UpdateAsset(UpdateAssetDto updateAssetDto);
        public Task<IEnumerable<AssetDto>> GetUserAssets(int id);
    }
}
