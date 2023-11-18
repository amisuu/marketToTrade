using Application.DTOs;
using Domain.Entities;
using Domain.Helpers;

namespace Application.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<AssetDto>> GetAllAssets();
        Task<AssetDto> GetAssetById(int id);
        Task<AddNewAssetDto> AddNewAsset(AddNewAssetDto user);
        Task<PhotoDto> AddPhoto(AssetDto asset, PhotoDto photo);
        Task<bool> UpdateAsset(UpdateAssetDto updateAssetDto);
        Task<IEnumerable<AssetDto>> GetUserAssets(int id);
        Task<PagedList<AssetDto>> GetPagedAssets(AssetParams assetParams);
        Task<bool> SaveAllAsync();
    }
}
