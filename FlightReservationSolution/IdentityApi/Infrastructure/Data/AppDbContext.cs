using IdentityApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Authentication;
using System.Security.Claims;

namespace IdentityApi.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>()
    {
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    }

    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {
                var admin = new AppUser
                {
                    FullName = "admin",
                    UserName = "admin@admin.com",
                    PasswordHash = "Admin@123",
                    Email = "admin@admin.com"

                };
                await userManager.CreateAsync(admin, admin.PasswordHash);
                List<Claim> claims = [
                    new(ClaimTypes.Role, Roles.Admin),
                    new(ClaimTypes.Email, admin.Email),
                    new(ClaimTypes.Name, admin.FullName),
                    new(Permissions.CanRead, true.ToString()),
                    new(Permissions.CanUpdate, true.ToString()),
                    new(Permissions.CanCreate, true.ToString()),
                    new(Permissions.CanDelete, true.ToString())
                    ];

                var _admin = await userManager.FindByEmailAsync(admin.Email);
                await userManager.AddClaimsAsync(_admin!, claims);

            }
        }
    }
}
