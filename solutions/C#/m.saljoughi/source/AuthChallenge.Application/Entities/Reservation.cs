namespace AuthChallenge.Application.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public required int ScreeningId { get; set; }
    public required string SeatNumber { get; set; }
    public ReservationStatus Status { get; set; }
}

public enum ReservationStatus
{
    Reserved,
    Rejected
}
