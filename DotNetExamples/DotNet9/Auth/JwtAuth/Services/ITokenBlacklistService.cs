namespace JwtAuth.Services
{
    public interface ITokenBlacklistService
    {
        Task<bool> IsTokenBlacklistedAsync(string token);
        Task BlacklistTokenAsync(string token);
    }
}
