using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    internal class LikesRepository : ILikesRepository
    {
        private readonly ApplicationDbContext _context;

        public LikesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AssetLike> GetAssetLike(int sourceAssetId, int likedAssetId)
        {
            return await _context.Likes.FindAsync(sourceAssetId, likedAssetId);
        }

        public async Task<IEnumerable<Asset>> GetAssetLikes(string predicate, int assetId)
        {
            var assets = _context.Assets.OrderBy(x => x.Price).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (predicate == "liked")
            {
                likes = likes.Where(like => like.SourceAssetId == assetId);
                assets = likes.Select(like => like.LikedAsset);
            }

            if (predicate == "likedBy")
            {
                likes = likes.Where(like => like.LikedAssetId == assetId);
                assets = likes.Select(like => like.SourceAsset);
            }

            return await assets.Select(asset => new Asset
            {
                Title = asset.Title,
                Mass = asset.Mass,
                AppUserId = asset.AppUserId,
                Form = asset.Form,
                PhotoUrl = asset.Photos.FirstOrDefault(x => x.IsMain).Url,
                Id = asset.Id
            }).ToListAsync();
        }

        public async Task<Asset> GetAssetWitkLikes(int assetId)
        {
            return await _context.Assets
                    .Include(x => x.LikedAssets)
                    .FirstOrDefaultAsync(x => x.Id == assetId);
        }
    }
}
