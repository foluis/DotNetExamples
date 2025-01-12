namespace JwtAuth.Services
{
    public class TokenBlacklistService //: ITokenBlacklistService
    {
        private static readonly HashSet<string> BlacklistedTokens = new HashSet<string>();

        public static Task<bool> IsTokenBlacklistedAsync(string token)
        {
            return Task.FromResult(BlacklistedTokens.Contains(token));
        }

        public static Task BlacklistTokenAsync(string token)
        {
            BlacklistedTokens.Add(token);
            return Task.CompletedTask;
        }
    }
}
