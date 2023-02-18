using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository _repository;
        private readonly IMapper _mapper;

        public LikesService(ILikesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AssetLikeDto> GetAssetLike(int sourceAssetId, int likedAssetId)
        {
            var asset = await _repository.GetAssetLike(sourceAssetId, likedAssetId);
            return _mapper.Map<AssetLikeDto>(asset);
        }

        public async Task<IEnumerable<AssetDto>> GetAssetLikes(string predicate, int userId)
        {
            var assets = await _repository.GetAssetLikes(predicate, userId);

            var likedAssets = assets.Select(asset => new AssetDto
            {
                PhotoUrl = asset.PhotoUrl,
                Price = asset.Price,
                Form = asset.Form
            });

            //return await PagedList<AssetDto>.CreateAsync(likedAssets, likesParams.PageNumber, likesParams.PageSize);
            var result = _mapper.Map<IEnumerable<AssetDto>>(likedAssets);
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetUserWithLike(string predicate, int userId)
        {
            var users = await _repository.GetUserWithLike(predicate, userId);

            var likedUsers = users.Select(user => new UserDto
            {
                Username = user.Username,
                KnownAs = user.KnownAs
            });

            //return await PagedList<UserDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
            var result = _mapper.Map<IEnumerable<UserDto>>(likedUsers);
            return result;
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            var user = await _repository.GetUserWithLikes(userId);
            return _mapper.Map<AppUser>(user);
        }

        public async Task AddLike(AssetLikeDto assetLikeDto, int userId)
        {
            var user = await GetUserWithLikes(userId);
            user.LikedAssets.Add(_mapper.Map<AssetLike>(assetLikeDto));
        }
    }
}
