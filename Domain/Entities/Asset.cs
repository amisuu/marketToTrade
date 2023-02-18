using Domain.Enums;

namespace Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Form { get; set; }
        public string Mass { get; set; }
        public string Fineness { get; set; }
        public int Quantity { get; set; }
        public string Producer { get; set; }
        public string Price { get; set; }
        public int Year { get; set; }
        public string PhotoUrl { get; set; }
        public string Condition { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string PricePerOZ { get; set; }
        public string PriceSPOT { get; set; }
        public YesOrNoEnum IsOriginalPackage { get; set; }
        public YesOrNoEnum IsReceipt { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public ICollection<Photo> Photos { get; set; }
        public AppUser Users { get; set; }
        public int AppUserId { get; set; }
        public ICollection<AssetLike> LikedByUsers { get; set; }
    }
}
