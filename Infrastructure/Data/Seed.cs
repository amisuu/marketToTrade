using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Infrastructure.Data
{
    public class Seed
    {
        private readonly IConfiguration _config;

        public Seed(IConfiguration config)
        {
            _config = config;
        }

        public async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var userData = await File.ReadAllTextAsync(_config.GetValue<string>("Secret:Users_Seed_Address"));

            var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);

            var roles = new List<AppRole>()
            {
                new AppRole { Name = "Admin"},
                new AppRole { Name = "Moderator"},
                new AppRole { Name = "Member"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                using var hmac = new HMACSHA256();

                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user, _config.GetValue<string>("Secret:PassEntry"));
                await userManager.AddToRoleAsync(user, "Member");
            }

            var adminUser = new AppUser
            {
                UserName = _config.GetValue<string>("Secret:Admin_Login")
            };

            await userManager.CreateAsync(adminUser, _config.GetValue<string>("Secret:PassEntry"));
            await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Moderator" });
        }

        public async Task SeedAssets(ApplicationDbContext context)
        {
            if (await context.Assets.AnyAsync())
            {
                return;
            }

            var assetData = await File.ReadAllTextAsync(_config.GetValue<string>("Secret:Assets_Seed_Address"));

            var assets = JsonConvert.DeserializeObject<List<Asset>>(assetData);

            foreach (var asset in assets)
            {
                context.Assets.Add(asset);
            }
            await context.SaveChangesAsync();
        }
    }
}
