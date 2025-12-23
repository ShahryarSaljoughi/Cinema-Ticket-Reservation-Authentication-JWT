using AuthChallenge.Application.Entities;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace AuthChallenge.Application.Services.Auth;

public class TokenService(AuthSettings authSettings) : ITokenService
{
    private AuthSettings AuthSettings { get; } = authSettings;

    public string GenerateAccessToken(User user)
    {
        var jwtSigningKey = AuthSettings.SigningKey;
        var jwtIssuer = AuthSettings.ValidIssuers;
        var jwtAudience = AuthSettings.ValidAudiences;

        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSigningKey);
        var creds = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature);
        var subjectClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username)
        };
        subjectClaims.AddRange(jwtAudience.Select(a => new Claim(JwtRegisteredClaimNames.Aud, a)));
        subjectClaims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));
        var scpClaimValue = string.Join(" ", user.Scopes);
        subjectClaims.Add(new Claim("scope", scpClaimValue));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(subjectClaims),
            Expires = DateTime.UtcNow.AddSeconds(AuthSettings.ExpirationInSeconds),
            Issuer = jwtIssuer.First(),
            SigningCredentials = creds,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

}
