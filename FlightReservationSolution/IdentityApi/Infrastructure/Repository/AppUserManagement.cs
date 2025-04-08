using Grpc.Core;
using IdentityApi.Domain;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Infrastructure.Repository
{
    public class AppUserManagement(UserManager<AppUser> userManger) : IAppUser
    {
        public async Task<bool> CreateAsync(AppUser user)
        {
            var result = await userManger.CreateAsync(user, user.PasswordHash!);
            if (result.Succeeded) { return true; }
            string error = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new RpcException(new Status(StatusCode.FailedPrecondition, error));
        }

        public async Task<AppUser?> GetByEmail(string email) => await userManger.FindByEmailAsync(email);

        public Task<AppUser?> GetById(string userId) => userManger.FindByIdAsync(userId);

        public async Task<bool> PasswordMatchAsync(AppUser user, string plainPassword)
        {
            bool result = await userManger.CheckPasswordAsync(user, plainPassword);
            return result;
        }
    }
}
