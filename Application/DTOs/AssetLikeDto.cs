using Domain.Entities;

namespace Application.DTOs
{
    public class AssetLikeDto
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public Asset LikedAsset { get; set; }
        public int LikedAssetId { get; set; }
    }
}
