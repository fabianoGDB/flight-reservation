using IdentityApi.Domain;

namespace IdentityApi.Infrastructure.Repository
{
    public interface IRefreshToken
    {
        Task<bool> IsTokenValid(string refreshToken);
        string GenerateToken();
        void AddToken(RefreshToken token);
        void UpdateToken(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
    }
}
