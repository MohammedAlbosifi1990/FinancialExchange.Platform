using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Entities;
using Shared.Core.Repositories;
using Shared.DataPersistence.Data.Db;

namespace Shared.DataPersistence.Repositories;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly DbSet<T> DbSet;


    public Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }

    public async Task<T?> SingleOrDefault(Guid id, bool asNoTracking = false)
    {
        var queryable = DbSet.AsQueryable();
        if (asNoTracking)
            queryable = DbSet.AsNoTracking();
        return await queryable.SingleOrDefaultAsync(e => e.Id == id);
    }

    public Task<IQueryable<T>> AsQueryable()
    {
        return Task.FromResult(DbSet.AsQueryable());
    }

    public async Task<List<T>> ToListAsync(
        Expression<Func<T, bool>>? expression = null,
        bool asNoTracking = false,
        params Expression<Func<T, object>>[] includes)
    {
        var queryable = DbSet.AsQueryable();
        if (asNoTracking)
            queryable = DbSet.AsNoTracking();

        if (expression != null)
            queryable = DbSet.Where(expression);
        if (includes.Any())
            queryable = includes.Aggregate(queryable,
                (current, includesExpression) => current.Include(includesExpression));
        var aa = await queryable.ToListAsync();
        return aa;
    }

    public async Task<T?> SingleOrDefault(Expression<Func<T, bool>> expression, bool asNoTracking = false,
        params Expression<Func<T, object>>[] includes)
    {
        var queryable = DbSet.AsQueryable();
        if (asNoTracking)
            queryable = DbSet.AsNoTracking();
        if (includes.Any())
            queryable = includes.Aggregate(queryable,
                (current, includesExpression) => current.Include(includesExpression));

        return await queryable.SingleOrDefaultAsync(expression);
    }

    public async Task<bool> Exist(Expression<Func<T, bool>> expression) =>
        await DbSet.AsNoTracking().AnyAsync(expression);

    public async Task<long> Count(Expression<Func<T, bool>> expression)
    {
        return await DbSet.CountAsync(expression);
    }

    public async Task<bool> Any(Expression<Func<T, bool>>? expression = null)
    {
        return
            expression == null
                ? await DbSet.AnyAsync()
                : await DbSet.AnyAsync(expression);
    }

    public async Task<List<T>> Find(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "",
        bool asNoTracking = false)
    {
        var queryable = DbSet.AsQueryable();
        if (asNoTracking)
            queryable = DbSet.AsNoTracking();

        queryable = queryable.Where(expression);

        // foreach (var includeProperty in includeProperties.Split
        //              (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        // {
        //     query = query.Include(includeProperty);
        // }
        queryable = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));

        return orderBy != null ? await orderBy(queryable).ToListAsync() : await queryable.ToListAsync();
    }

    public async Task<T> Add(T entity)
    {
        var entryEntity = await DbSet.AddAsync(entity);
        return entryEntity.Entity;
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public Task Remove(T entity)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public async Task Commit(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}