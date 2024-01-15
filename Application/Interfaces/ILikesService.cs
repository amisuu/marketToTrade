using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILikesService
    {
        Task AddLike(AssetLikeDto assetLikeDto, int userId);
        Task<AssetLikeDto> GetAssetLike(int sourceAssetId, int likedAssetId);
        Task<IEnumerable<AssetDto>> GetAssetLikes(string predicate, int userId);
        Task<IEnumerable<UserDto>> GetUserWithLike(string predicate, int userId);
        Task<AppUser> GetUserWithLikes(int assetId);
        //string predicate, int assetId
        Task<bool> SaveAllAsync();
    }
}
