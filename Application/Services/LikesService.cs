using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class LikesService : ILikesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LikesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<AssetLikeDto> GetAssetLike(int sourceAssetId, int likedAssetId)
        {
            var asset = await _unitOfWork.LikesRepository.GetAssetLike(sourceAssetId, likedAssetId);
            return _mapper.Map<AssetLikeDto>(asset);
        }

        public async Task<IEnumerable<AssetDto>> GetAssetLikes(string predicate, int userId)
        {
            var assets = await _unitOfWork.LikesRepository.GetAssetLikes(predicate, userId);

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
            var users = await _unitOfWork.LikesRepository.GetUserWithLike(predicate, userId);

            var likedUsers = users.Select(user => new UserDto
            {
                Username = user.UserName,
                KnownAs = user.KnownAs
            });

            //return await PagedList<UserDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
            var result = _mapper.Map<IEnumerable<UserDto>>(likedUsers);
            return result;
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            var user = await _unitOfWork.LikesRepository.GetUserWithLikes(userId);
            return _mapper.Map<AppUser>(user);
        }

        public async Task AddLike(AssetLikeDto assetLikeDto, int userId)
        {
            var user = await GetUserWithLikes(userId);
            user.LikedAssets.Add(_mapper.Map<AssetLike>(assetLikeDto));
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _unitOfWork.Complete();
        }
    }
}
