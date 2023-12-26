using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {//ponieważ dodawana jest tabela pośrednia AppUserRole dodane zostały pozostałem Identity...
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
              .HasMany(u => u.UserRoles)
              .WithOne(u => u.Role)
              .HasForeignKey(u => u.RoleId)
              .IsRequired();

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
