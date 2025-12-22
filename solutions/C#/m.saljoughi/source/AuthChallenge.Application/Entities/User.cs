using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Application.Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Name { get; set; }
    public string? PasswordHash { get; set; }

    public List<string> Scopes { get; set; } = [];
}
