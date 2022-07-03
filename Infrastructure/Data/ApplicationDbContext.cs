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
                .HasKey(key => new {key.SourceAssetId, key.LikedAssetId});

            modelBuilder.Entity<AssetLike>()
                .HasOne(source => source.SourceAsset)
                .WithMany(like => like.LikedAssets)
                .HasForeignKey(key => key.SourceAssetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AssetLike>()
                .HasOne(source => source.LikedAsset)
                .WithMany(like => like.LikedByUsers)
                .HasForeignKey(key => key.LikedAssetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
