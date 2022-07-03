using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

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

        public async Task<IEnumerable<LikeDto>> GetAssetLikes(string predicate, int assetId)
        {
            var assets = await _repository.GetAssetLikes(predicate, assetId);
            return _mapper.Map<IEnumerable<LikeDto>>(assets);
        }

        public async Task<AssetDto> GetAssetWitkLikes(int assetId)
        {
            var asset = await _repository.GetAssetWitkLikes(assetId);
            return _mapper.Map<AssetDto>(asset);
        }
    }
}
