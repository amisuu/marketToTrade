using Application.DTOs;

namespace Application.Interfaces
{
    public interface ILikesService
    {
        public Task<AssetLikeDto> GetAssetLike(int sourceAssetId, int likedAssetId);
        public Task<AssetDto> GetAssetWitkLikes(int assetId);
        Task<IEnumerable<LikeDto>> GetAssetLikes(string predicate, int assetId);
    }
}
