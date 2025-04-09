namespace IdentityApi.Infrastructure.Repository
{
    public interface IUnityOfWork
    {
        IJwtToken JwtToken { get; }
        IRefreshToken RefreshToken { get; }
        IAppUser AppUser { get; }
        IClaim Claim { get; }

        Task SaveChangesAsync();

    }
}
