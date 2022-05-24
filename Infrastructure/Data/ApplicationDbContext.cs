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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .Property(u => u.IsReceipt)
                .HasConversion<string>()
                .HasMaxLength(3);

            modelBuilder.Entity<Asset>()
                .Property(u => u.IsOriginalPackage)
                .HasConversion<string>()
                .HasMaxLength(3);
        }
    }
}
