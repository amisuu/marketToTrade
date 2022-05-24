using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    internal class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext _context;

        public AssetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {
            return await _context.Assets
                    .Include(x => x.Photos)
                    .ToListAsync();
        }

        public async Task<Asset> GetAssetById(int id)
        {
            return await _context.Assets
                    .Include(x => x.Photos)
                    .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Asset> Add(Asset userAsset)
        {
            _context.Assets.Add(userAsset);
            await _context.SaveChangesAsync();

            return userAsset;
        }

        public async Task<Photo> AddPhoto(Asset asset, Photo photo)
        {
            asset.Photos.Add(photo);

            if (asset.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            await _context.SaveChangesAsync();

            return photo;
        }
    }
}
