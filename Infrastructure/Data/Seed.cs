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

            var userData = await File.ReadAllTextAsync(Environment.GetEnvironmentVariable("Users_Seed_Address"));
            
            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA256();

                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Password_User")));
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

            var assetData = await File.ReadAllTextAsync(Environment.GetEnvironmentVariable("Assets_Seed_Address"));

            var assets = JsonConvert.DeserializeObject<List<Asset>>(assetData);

            foreach (var asset in assets)
            {
                context.Assets.Add(asset);
            }
            await context.SaveChangesAsync();
        }
    }
}
