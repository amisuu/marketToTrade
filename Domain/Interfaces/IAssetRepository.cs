using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAssetRepository
    {
        public Task<IEnumerable<Asset>> GetAssets();
        public Task<Asset> GetAssetById(int id);
        public Task<Asset> Add(Asset userAsset);
        public Task<Photo> AddPhoto(Asset asset, Photo photo);
        public void Update(Asset asset);
        public Task<IEnumerable<Asset>> GetUserAssets(int id);
    }
}
