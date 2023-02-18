using Application.DTOs;
using Domain.Entities;
using Domain.Helpers;

namespace Application.Interfaces
{
    public interface ILikesService
    {
        Task<AssetLikeDto> GetAssetLike(int sourceAssetId, int likedAssetId);
        Task<AppUser> GetUserWithLikes(int assetId);
        //string predicate, int assetId
        Task<IEnumerable<AssetDto>> GetAssetLikes(string predicate, int userId);
        Task<IEnumerable<UserDto>> GetUserWithLike(string predicate, int userId);
        Task AddLike(AssetLikeDto assetLikeDto, int userId);
    }
}
