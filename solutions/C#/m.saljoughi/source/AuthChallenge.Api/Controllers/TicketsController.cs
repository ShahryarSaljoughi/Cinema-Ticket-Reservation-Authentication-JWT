using AuthChallenge.Api.ApiModels;
using AuthChallenge.Application.Abstractions;
using AuthChallenge.Application.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthChallenge.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    public TicketsController(IReservationRepository reservationRepo, IUnitOfWork unitOfWork)
    {
        ReservationRepo = reservationRepo;
        UnitOfWork = unitOfWork;
    }

    public IReservationRepository ReservationRepo { get; }
    public IUnitOfWork UnitOfWork { get; }

    [HttpPost, Authorize]
    public async Task<ActionResult<ReserveResponse>> ReserveAsync(ReserveRequest request, CancellationToken cancellation)
    {
        var reservation = new Reservation
        {
            ScreeningId = request.ScreeningId,
            SeatNumber = request.SeatNumber,
            Id = Guid.NewGuid(),
            Status = ReservationStatus.Reserved
        };
        await ReservationRepo.AddAsync(reservation);
        await UnitOfWork.SaveChangesAsync(cancellation);
        return Ok(new ReserveResponse(ReservationId: reservation.Id, Status: reservation.Status));
    }
}
