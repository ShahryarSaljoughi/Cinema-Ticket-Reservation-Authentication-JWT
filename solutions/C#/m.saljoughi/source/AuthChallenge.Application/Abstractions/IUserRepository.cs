using AuthChallenge.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Application.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}
