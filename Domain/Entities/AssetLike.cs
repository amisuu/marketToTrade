namespace Domain.Entities
{
    public class AssetLike
    {
        public Asset SourceAsset { get; set; }
        public int SourceAssetId { get; set; }
        public Asset LikedAsset { get; set; }
        public int LikedAssetId { get; set; }
    }
}
