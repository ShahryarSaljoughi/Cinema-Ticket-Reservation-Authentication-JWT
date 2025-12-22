using AuthChallenge.Application.Entities;

namespace AuthChallenge.Application.Services.Auth;

public interface ITokenService
{
    string GenerateAccessToken(User user);
}