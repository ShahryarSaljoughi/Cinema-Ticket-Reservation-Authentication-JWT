using AuthChallenge.Api.ApiModels;
using AuthChallenge.Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthChallenge.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    public AuthController(IAuthService authService, AuthSettings authSettings)
    {
        AuthService = authService;
        AuthSettings = authSettings;
    }

    public IAuthService AuthService { get; }
    public AuthSettings AuthSettings { get; }

    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var result = await AuthService.LoginAsync(
            new LoginInput
            {
                Password = request.Password,
                Username = request.Username
            });
        if (result is null)
        {
            return Unauthorized();
        }

        return Ok(new LoginResponse
        (
            AccessToken: result.AccessToken,
            ExpiresInSeconds: AuthSettings.ExpirationInSeconds,
            TokenType: "Bearer"
        ));
    }
}
