using Domain.Entities;

namespace Application.DTOs
{
    public class AssetLikeDto
    {
        public Asset SourceAsset { get; set; }
        public int SourceAssetId { get; set; }
        public Asset LikedAsset { get; set; }
        public int LikedAssetId { get; set; }
    }
}
