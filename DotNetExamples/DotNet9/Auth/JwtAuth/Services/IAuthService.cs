using JwtAuth.Entities;
using JwtAuth.Models;

namespace JwtAuth.Services
{
    public interface IAuthService
    {
        //Task<Result<User>> RegisterAsync(User user);
        Task<User?> RegisterAsync(UserDto user);
        Task<TokenResponseDto?> LoginAsync(UserDto user);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        //Task<string> Logout(string userId);
    }
}
