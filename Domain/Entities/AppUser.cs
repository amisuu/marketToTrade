namespace Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public int Phone { get; set; }
        public string Email { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<AssetLike> LikedAssets { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
    }
}
