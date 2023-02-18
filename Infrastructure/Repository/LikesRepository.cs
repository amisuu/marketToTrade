using Application.DTOs;
using Domain.Entities;
using Domain.Helpers;
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

        public async Task<IQueryable<Asset>> GetAssetLikes(string predicate, int userId)
        {
            var assets = _context.Assets.OrderBy(x => x.Price).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == userId);
                assets = likes.Select(like => like.LikedAsset); //asssety ktore sa polubione
            }
            
            return assets;
        }

        public async Task<IQueryable<AppUser>> GetUserWithLike(string predicate, int userId)
        {
            var likes = _context.Likes.AsQueryable();
            var users = _context.Users.AsQueryable();

            if (predicate == "likedBy")
            {
                likes = likes.Where(like => like.LikedAssetId == userId);
                users = likes.Select(asset => asset.SourceUser);
                //return (IEnumerable<T>)await users.Select(user => new LikeDto
                //{
                //    Username = user.Username,
                //    KnownAs = user.KnownAs,
                //    Phone = user.Phone,
                //    Email = user.Email
                //}).ToListAsync();
            }

            return users;
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                    .Include(x => x.LikedAssets)
                    .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
