using AuthChallenge.Application.Abstractions;
using AuthChallenge.Application.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using BCrypt.Net;

namespace AuthChallenge.Application.Services.Auth;

public class AuthService(ITokenService tokenService, IUserRepository userRepository) : IAuthService
{
    private ITokenService TokenService { get; } = tokenService;
    private IUserRepository UserRepository { get; } = userRepository;

    public async Task<LoginOutput?> LoginAsync(LoginInput input)
    {
        var user = await ValidateUserAsync(input.Username, input.Password);
        if (user is null) return null;
        var token = TokenService.GenerateAccessToken(user);

        return new LoginOutput
        {
            AccessToken = token,
        };
    }

    private async Task<User?> ValidateUserAsync(string username, string password)
    {
        var user = await UserRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return null;
        }

        var passwordVerified = VerifyPassword(password, user.PasswordHash);
        if (!passwordVerified)
        {
            return null;
        }
        return user;
    }

    private bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    private string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}

public class LoginInput
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginOutput
{
    public string AccessToken { get; internal set; }
}
