using JwtAuth.Data;
using JwtAuth.Entities;
using JwtAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuth.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<TokenResponseDto?> LoginAsync(UserDto userDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserName);

            if (user is null)            
                return null;            

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, userDto.Password)
                == PasswordVerificationResult.Failed)            
                return null;            

            return await CreateTokenResponseAsync(user);
        }
        
        public async Task<User?> RegisterAsync(UserDto userDto)
        {
            if (await context.Users.AnyAsync(u => u.UserName == userDto.UserName))
            {
                return null;
            }

            var user = new User();

            var hashedPassword = new PasswordHasher<User>().HashPassword(null, userDto.Password);

            user.UserName = userDto.UserName;
            user.PasswordHash = hashedPassword;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        //public Task<string>? Logout(string userId)
        //{          
        //    return DeleteRefreshToken(userId);
        //}

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);

            if (user is null)
                return null;

            return await CreateTokenResponseAsync(user);
        }

        public async Task<User?> ValidateRefreshTokenAsync(Guid usedId, string refreshToken)
        {
            var user = await context.Users.FindAsync(usedId);
            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTimeExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        //private async Task<string> DeleteRefreshToken(string userId)
        //{
        //    var user = await context.Users.FindAsync(new Guid(userId));

        //    user!.RefreshToken = null;
        //    user.RefreshTimeExpiryTime = DateTime.MinValue;
        //    context.SaveChanges();

        //    return "RefreshToken";
        //}

        private async Task<TokenResponseDto> CreateTokenResponseAsync(User? user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User? user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTimeExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(User? user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role,user.Role)
            };

            //Nuget for [SymmetricSecurityKey] -> System.IdentityModel.Tokens.Jwt
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); //key has to be at least 64 byts/characters

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }




    }
}