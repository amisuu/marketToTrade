namespace Application.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string KnownAs { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Created { get; set; } 
        public DateTime LastActive { get; set; } 
        public int Phone { get; set; }
        public string Email { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public ICollection<AssetDto> Assets { get; set; }
    }
}
