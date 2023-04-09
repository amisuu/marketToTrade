using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string Title { get; set; } 
        public string Type { get; set; }
        public string Form { get; set; }
        public string Mass { get; set; }
        public string Fineness { get; set; }
        public int Quantity { get; set; }
        public string Producer { get; set; }
        public string Price { get; set; }
        public int Year { get; set; }
        public string Condition { get; set; }
        public YesOrNoEnum IsOriginalPackage { get; set; }
        public YesOrNoEnum IsReceipt { get; set; }
        public DateTime PublicationDate { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public ICollection<AssetLikeDto> LikedByUsers { get; set; }
        public ICollection<AssetLikeDto> LikedAssets { get; set; }
    }
}
