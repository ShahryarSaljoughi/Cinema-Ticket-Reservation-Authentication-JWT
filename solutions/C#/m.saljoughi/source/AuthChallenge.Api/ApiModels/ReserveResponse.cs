using AuthChallenge.Application.Entities;

namespace AuthChallenge.Api.ApiModels;

public record ReserveResponse(Guid ReservationId, ReservationStatus Status);


