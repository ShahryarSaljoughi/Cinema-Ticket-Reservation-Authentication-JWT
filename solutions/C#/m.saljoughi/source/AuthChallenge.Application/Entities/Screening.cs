using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Application.Entities;

public class Screening
{
    public int Id { get; set; }
    public required string MovieTitle { get; set; }
    public DateTimeOffset StartsAt { get; set; }
    public string Hall { get; set; }
}
