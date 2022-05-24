using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAssetService
    {
        public Task<IEnumerable<AssetDto>> GetAllAssets();
        public Task<AssetDto> GetAssetById(int id);
        public Task<AssetDto> AddNewAsset(AssetDto user);
        public Task<PhotoDto> AddPhoto(AssetDto asset, PhotoDto photo);
        public PhotoDto MapPhotoToDto(Photo photo);
    }
}
