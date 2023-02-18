namespace Domain.Entities
{
    public class AssetLike
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public Asset LikedAsset { get; set; }
        public int LikedAssetId { get; set; }
    }
}
