using Application.DTOs;
using Domain.Helpers;

namespace Application.Interfaces
{
    public interface IPagedListRepository
    {
        Task<PagedList<AssetDto>> GetPagedAssets(AssetParams assetParams);
    }
}
