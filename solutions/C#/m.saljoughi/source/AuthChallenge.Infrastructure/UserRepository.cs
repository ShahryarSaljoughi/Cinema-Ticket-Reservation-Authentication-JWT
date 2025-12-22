using AuthChallenge.Application.Abstractions;
using AuthChallenge.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Infrastructure;

public class UserRepository(AppDbContext appDb) : IUserRepository
{
    public AppDbContext AppDb { get; } = appDb;

    public Task<User?> GetByUsernameAsync(string username)
    {
        return AppDb.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
    }
}
