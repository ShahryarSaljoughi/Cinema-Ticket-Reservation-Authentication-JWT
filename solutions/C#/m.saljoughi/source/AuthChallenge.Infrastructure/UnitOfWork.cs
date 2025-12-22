using AuthChallenge.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public Task<int> SaveChangesAsync(CancellationToken cancellation) => _context.SaveChangesAsync(cancellation);
}