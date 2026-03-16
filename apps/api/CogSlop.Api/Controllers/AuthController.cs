using CogSlop.Api.Models.Dtos;
using CogSlop.Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CogSlop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    ICurrentUserService currentUserService,
    IConfiguration configuration) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = "/api/auth/post-login",
        };

        return Challenge(properties);
    }

    [Authorize]
    [HttpGet("post-login")]
    public async Task<IActionResult> PostLogin(CancellationToken cancellationToken)
    {
        await currentUserService.EnsureUserAsync(User, cancellationToken);

        var frontendBaseUrl = configuration["Frontend:BaseUrl"] ?? "http://localhost:4200";
        return Redirect($"{frontendBaseUrl.TrimEnd('/')}/");
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserProfileDto>> Me(CancellationToken cancellationToken)
    {
        var profile = await currentUserService.GetProfileAsync(User, cancellationToken);
        return Ok(profile);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { message = "Cog clutch released. You are logged out." });
    }
}
