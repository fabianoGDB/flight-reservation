using IdentityApi.Domain;
using IdentityApi.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Infrastructure.Repository
{
    public class RepositoryManagement : IUnityOfWork
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtToken _jwtToken;
        private readonly IRefreshToken _refreshToken;
        private readonly IAppUser _appUser;
        private readonly IClaim _claim;

        public RepositoryManagement(AppDbContext context, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _jwtToken = new JwtTokenManagement(configuration);
            _refreshToken = new RefreshTokenManagement(context);
            _appUser = new AppUserManagement(_userManager);
            _claim = new ClaimManagement(_userManager);
        }

        public IJwtToken JwtToken => _jwtToken;

        public IRefreshToken RefreshToken => _refreshToken;

        public IAppUser AppUser => _appUser;

        public IClaim Claim => _claim;

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
