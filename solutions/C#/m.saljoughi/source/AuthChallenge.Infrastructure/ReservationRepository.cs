using AuthChallenge.Application.Abstractions;
using AuthChallenge.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Infrastructure;

public class ReservationRepository(AppDbContext appDb) : 
    GenericRepository<Reservation, Guid>(appDb), IReservationRepository
{
}
