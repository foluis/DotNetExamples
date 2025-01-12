using JwtAuth.Entities;
using JwtAuth.Models;
using JwtAuth.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto userDto)
        {
            var user = await authService.RegisterAsync(userDto);

            if (user is null)
                return BadRequest("User already exists");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] UserDto userDto)
        {
            var tokenResponse = await authService.LoginAsync(userDto);

            if (tokenResponse is null)
                return BadRequest("Invalid username or password");

            return Ok(tokenResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is missing.");
            }

            // Blacklist the token
            await TokenBlacklistService.BlacklistTokenAsync(token);

            return Ok("Logged out successfully.");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null 
                || result.AccessToken is null
                || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token");

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("admin-only")]
        public ActionResult AdminOnlyEndpoint()
        {
            return Ok("You are an admin");
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
