using AuthChallenge.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Infrastructure;

public class AppDbContext: DbContext
{
    public DbSet<Screening> Screenings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
}
