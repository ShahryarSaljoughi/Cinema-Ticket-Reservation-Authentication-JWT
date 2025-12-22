
namespace AuthChallenge.Application.Services.Auth;

public interface IAuthService
{
    Task<LoginOutput?> LoginAsync(LoginInput input);
}