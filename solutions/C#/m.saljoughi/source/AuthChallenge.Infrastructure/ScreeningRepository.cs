using AuthChallenge.Application.Abstractions;
using AuthChallenge.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthChallenge.Infrastructure;

public class ScreeningRepository : GenericRepository<Screening, int>, IScreeningRepository
{
    public ScreeningRepository(AppDbContext appDb) 
        : base(appDb)
    {
    }


    public Task<Screening[]> GetAllAsync()
    {
        return AppDb.Set<Screening>().ToArrayAsync();
    }
}
