using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILikesRepository
    {
        public Task<AssetLike> GetAssetLike(int sourceAssetId, int likedAssetId);
        public Task<Asset> GetAssetWitkLikes(int assetId);
        Task<IEnumerable<Asset>> GetAssetLikes(string predicate, int assetId);
    }
}
