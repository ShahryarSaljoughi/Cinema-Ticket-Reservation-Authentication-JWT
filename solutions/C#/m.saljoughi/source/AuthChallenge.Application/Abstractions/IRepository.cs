using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AuthChallenge.Application.Abstractions;

public interface IRepository<T, TId>
{
    public Task<T?> GetByIdAsync(TId id);
    public Task AddAsync(T entity);
    public Task AddRangeAsync(params IEnumerable<T> entities);
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter);
    public void Detach(T entity);
    void AddWithoutRelatedEntities(T entity);
    Task<List<T>> GetListAsync(Expression<Func<T, bool>>[] filters);
    Task<(List<T> Items, int TotalCount)> GetListAsync<TOrderKey>(Expression<Func<T, bool>>[] filters,
                                                                  int pageSize,
                                                                  int page,
                                                                  Expression<Func<T, TOrderKey>> orderBy,
                                                                  bool isDescending);
}
