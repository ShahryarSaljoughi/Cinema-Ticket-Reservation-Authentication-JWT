using System;
using System.Collections.Generic;
using System.Text;

namespace AuthChallenge.Application.Abstractions;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellation);
}
