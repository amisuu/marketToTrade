using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedUsers(ApplicationDbContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }

            var userData = await File.ReadAllTextAsync(@"D:\Projekty1\marketToTrade\Infrastructure\Data\UsersDataSeed.json");
            
            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA256();

                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedAssets(ApplicationDbContext context)
        {
            if (await context.Assets.AnyAsync())
            {
                return;
            }

            var assetData = await File.ReadAllTextAsync(@"D:\marketToTrade\Infrastructure\Data\AssetSeed.json");

            var assets = JsonConvert.DeserializeObject<List<Asset>>(assetData);

            foreach (var asset in assets)
            {
                context.Assets.Add(asset);
            }
            await context.SaveChangesAsync();
        }
    }
}
