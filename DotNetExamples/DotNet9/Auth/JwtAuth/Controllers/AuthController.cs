using JwtAuth.Entities;
using JwtAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        public static User user = new();

        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] UserDto userDto)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(null, userDto.Password);

            user.UserName = userDto.UserName;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string>Login([FromBody] UserDto userDto)
        {
            if (user.UserName != userDto.UserName)
            {
                return BadRequest("Invalid username - not found");
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(null, user.PasswordHash, userDto.Password) 
                == PasswordVerificationResult.Failed)
            {
                return BadRequest("Invalid username - wrong password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
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

        //private readonly IAuthService _authService;
        //private readonly IMapper _mapper;
        //private readonly IConfiguration _configuration;
        //public AuthController(IAuthService authService, IMapper mapper, IConfiguration configuration)
        //{
        //    _authService = authService;
        //    _mapper = mapper;
        //    _configuration = configuration;
        //}
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] UserDto userDto)
        //{
        //    var user = _mapper.Map<User>(userDto);
        //    var result = await _authService.RegisterAsync(user);
        //    if (!result.Success)
        //    {
        //        return BadRequest(result.Message);
        //    }
        //    return Ok(result.Data);
        //}
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] UserDto userDto)
        //{
        //    var result = await _authService.LoginAsync(userDto.UserName, userDto.Password);
        //    if (!result.Success)
        //    {
        //        return BadRequest(result.Message);
        //    }
        //    var token = _authService.GenerateToken(result.Data);
        //    return Ok(token);
        //}
        //[HttpGet("refresh")]
        //public async Task<IActionResult> Refresh()
        //{
        //    var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        //    var result = await _authService.RefreshTokenAsync(token);
        //    if (!result.Success)
        //    {
        //        return BadRequest(result.Message);
        //    }
        //    return Ok(result.Data);
        //}
    }
}
