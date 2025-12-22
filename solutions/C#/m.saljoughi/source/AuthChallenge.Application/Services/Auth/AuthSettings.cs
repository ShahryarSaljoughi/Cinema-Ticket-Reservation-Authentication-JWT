using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Application.Services.Auth;

public class AuthSettings
{
    public List<string> ValidAudiences { get; set; } = new();
    public List<string> ValidIssuers { get; set; } = new();
    public string SigningKey { get; set; }
    public long ExpirationInSeconds { get; set; } = 3600;

}
