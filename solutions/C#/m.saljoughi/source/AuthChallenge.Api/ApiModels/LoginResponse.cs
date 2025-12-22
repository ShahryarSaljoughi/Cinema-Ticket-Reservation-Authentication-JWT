namespace AuthChallenge.Api.ApiModels;

public record LoginResponse(string AccessToken, long ExpiresInSeconds, string TokenType = "Bearer");
