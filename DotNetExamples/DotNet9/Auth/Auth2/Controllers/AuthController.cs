using Auth2.Entities;
using Auth2.Models;
using Auth2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register([FromBody] UserDto userDto)
        //{
        //    var user = await authService.RegisterAsync(userDto);

        //    if (user is null)
        //        return BadRequest("User already exists");

        //    return Ok(user);
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto model)
        {
            var user = new User { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            // Assign the "Client" role to the user
            var roleResult = await _userManager.AddToRoleAsync(user, "Client");

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Add a claim for the app the user belongs to
            var appClaim = new Claim("App", "App Name"/*model.AppName*/); // Replace "App" with a custom key if needed
            var claimResult = await _userManager.AddClaimAsync(user, appClaim);

            if (!claimResult.Succeeded)
            {
                return BadRequest(claimResult.Errors);
            }

            return Ok(new { Message = "User registered successfully" });
        }

        //[HttpPost("login")]
        //public async Task<ActionResult<TokenResponseDto>> Login([FromBody] UserDto userDto)
        //{
        //    var tokenResponse = await authService.LoginAsync(userDto);

        //    if (tokenResponse is null)
        //        return BadRequest("Invalid username or password");

        //    return Ok(tokenResponse);
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDto model)
        {
            //var user = new User();
            var user = await _userManager.FindByNameAsync(model.Username);
            //user = user1;
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();
            }

            var token = await _tokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }

        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    var token = HttpContext.Request.Headers["Authorization"].ToString()?.Split(" ").Last();

        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return BadRequest("Token is missing.");
        //    }

        //    // Blacklist the token
        //    await TokenBlacklistService.BlacklistTokenAsync(token);

        //    return Ok("Logged out successfully.");
        //}

        //[HttpPost("refresh-token")]
        //public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        //{
        //    var result = await authService.RefreshTokensAsync(request);
        //    if (result is null
        //        || result.AccessToken is null
        //        || result.RefreshToken is null)
        //        return Unauthorized("Invalid refresh token");

        //    return Ok(result);
        //}

        [AllowAnonymous]
        [HttpGet("Anonymous")]
        public ActionResult AnonymousEndpoint()
        {
            return Ok("Everyone can see this");
        }

        [Authorize]
        [HttpGet("Authenticated")]
        public ActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminOnly")]
        public ActionResult AdminOnlyEndpoint()
        {
            return Ok("You are an admin");
        }

        [Authorize(Roles = "Admin,Client")]
        [HttpGet("AdminAndClientsOnly")]
        public ActionResult AdminAndClientsOnly()
        {
            return Ok("You are an admin or a client");
        }

        [Authorize]
        [HttpGet("claims")]
        public IActionResult GetUserClaims()
        {
            var appClaim = User.Claims.FirstOrDefault(c => c.Type == "App")?.Value;

            // Retrieve all claims of the currently authenticated user
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            return Ok(new
            {
                Message = "User claims retrieved successfully",
                Claims = claims,
                Roles = roles
            });
        }
    }
}
