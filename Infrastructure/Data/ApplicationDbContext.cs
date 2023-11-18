using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>()
                .Property(u => u.IsReceipt)
                .HasConversion<string>()
                .HasMaxLength(3);

            modelBuilder.Entity<Asset>()
                .Property(u => u.IsOriginalPackage)
                .HasConversion<string>()
                .HasMaxLength(3);

            modelBuilder.Entity<AssetLike>()
                .HasKey(key => new { key.SourceUserId, key.LikedAssetId });

            modelBuilder.Entity<AssetLike>()
                .HasOne(source => source.SourceUser)
                .WithMany(like => like.LikedAssets)
                .HasForeignKey(key => key.SourceUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AssetLike>()
                .HasOne(source => source.LikedAsset)
                .WithMany(like => like.LikedByUsers)
                .HasForeignKey(key => key.LikedAssetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Photo>()
                .HasOne(u => u.Asset)
                .WithMany(u => u.Photos)
                .HasForeignKey(u => u.AssetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Photo>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Photos)
                .HasForeignKey(u => u.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(s => s.Receipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict); //After user delete profile, message should stay for other person

            modelBuilder.Entity<Message>()
                .HasOne(s => s.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
