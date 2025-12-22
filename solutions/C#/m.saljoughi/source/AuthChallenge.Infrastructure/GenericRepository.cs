using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;

namespace AuthChallenge.Infrastructure;

public class GenericRepository<TEntity, TId> where TEntity : class
{
    public GenericRepository(AppDbContext applicationDb)
    {
        AppDb = applicationDb;
    }

    public AppDbContext AppDb { get; }

    public virtual async Task AddAsync(TEntity entity)
    {
        await AppDb.Set<TEntity>().AddAsync(entity);
    }

    public virtual void AddWithoutRelatedEntities(TEntity entity)
    {
        AppDb.Set<TEntity>().Entry(entity).State = EntityState.Added;
    }

    public virtual async Task AddRangeAsync(params IEnumerable<TEntity> entities)
    {
        await AppDb.Set<TEntity>().AddRangeAsync(entities);
    }

    public void Detach(TEntity entity)
    {
        AppDb.Set<TEntity>().Entry(entity).State = EntityState.Detached;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await AppDb.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await AppDb.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>[] filters)
    {
        var query = AppDb.Set<TEntity>().AsQueryable();
        foreach (var item in filters)
        {
            query = query.Where(item);
        }

        return await query.ToListAsync();
    }

    public async Task<(List<TEntity> Items, int TotalCount)> GetListAsync<TOrderKey>(Expression<Func<TEntity, bool>>[] filters,
                                                             int pageSize,
                                                             int page,
                                                             Expression<Func<TEntity, TOrderKey>> orderBy,
                                                             bool isDescending)
    {
        var query = AppDb.Set<TEntity>().AsQueryable();
        foreach (var item in filters)
        {
            query = query.Where(item);
        }
        var count = await query.CountAsync();
        var pageResultQuery = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
        return (await pageResultQuery.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync(), count);
    }
}
