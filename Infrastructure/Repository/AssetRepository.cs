using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AssetRepository : IAssetRepository, IPagedListRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AssetRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<Asset> GetAssetById2(int id)
        {
            return await _context.Assets
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

        public void Update(Asset asset)
        {
            _context.Entry(asset).State = EntityState.Modified;
        }
        
        public async Task<IEnumerable<Asset>> GetUserAssets(int id)
        {
            return await _context.Assets.Where(x => x.AppUserId == id).ToListAsync();
        }

        public async Task<PagedList<AssetDto>> GetPagedAssets(AssetParams assetParams)
        {
            var query = _context.Assets.ProjectTo<AssetDto>(_mapper.ConfigurationProvider)
                                       .AsNoTracking();

            if (assetParams.Search != null)
            {
                query = query.Where(x => x.Title.ToLower().Contains(assetParams.Search));
            }

            query = assetParams.OrderBy switch
            {
                _ => query.OrderByDescending(x => x.PublicationDate),
            };

            return await PagedList<AssetDto>.CreateAsync(query, assetParams.PageNumber, assetParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
