using System.Linq.Expressions;
using Shared.Core.Domain.Entities;

namespace Shared.Core.Repositories;

public interface IGenericRepository<T>
    where T : class, IBaseEntity
{
    Task<T?> SingleOrDefault(Guid id,bool asNoTracking=false);
    Task<IQueryable<T>> AsQueryable();
    Task<List<T>> ToListAsync( 
        Expression<Func<T, bool>>? expression =null,
        bool asNoTracking=false,
        params Expression<Func<T, object>>[] includes);

    Task<T?> SingleOrDefault(
        Expression<Func<T, bool>> expression,
        bool asNoTracking=false,
        params Expression<Func<T, object>>[] includes);
    Task<bool> Exist(Expression<Func<T, bool>> expression);
    Task<long> Count(Expression<Func<T, bool>> expression);
    Task<bool> Any(Expression<Func<T, bool>>? expression=null);
    Task<List<T>> Find(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "",
        bool asNoTracking = false);

    Task<T> Add(T entity);
    Task AddRange(List<T> entities);
    Task Remove(T entity);
    Task RemoveRange(List<T> entities);
    Task Commit(CancellationToken cancellationToken=default);
}