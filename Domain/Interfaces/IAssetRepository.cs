using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAssets();
        Task<Asset> GetAssetById(int id);
        Task<Asset> GetAssetById2(int id);
        Task<Asset> Add(Asset userAsset);
        Task<Photo> AddPhoto(Asset asset, Photo photo);
        void Update(Asset asset);
        Task<IEnumerable<Asset>> GetUserAssets(int id);
        Task<bool> SaveAllAsync();
    }
}
