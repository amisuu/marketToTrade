using Domain.Entities;

namespace Application.DTOs
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public int Phone { get; set; }
        public string Email { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public ICollection<AssetDto> Assets { get; set; }
        public ICollection<AssetLikeDto> LikedAssets { get; set; }
    }
}
