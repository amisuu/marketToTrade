using Domain.Entities;
using Domain.Helpers;

namespace Domain.Interfaces
{
    public interface ILikesRepository
    {
        public Task<AssetLike> GetAssetLike(int sourceAssetId, int likedAssetId);
        public Task<AppUser> GetUserWithLikes(int assetId);
        public Task<IQueryable<Asset>> GetAssetLikes(string predicate, int userId);
        public Task<IQueryable<AppUser>> GetUserWithLike(string predicate, int userId);
    }
}
