namespace AuthChallenge.Api.ApiModels.DTOs;

public class ScreeningDto
{
    public int Id { get; set; }
    public required string MovieTitle { get; set; }
    public DateTimeOffset StartsAt { get; set; }
}
