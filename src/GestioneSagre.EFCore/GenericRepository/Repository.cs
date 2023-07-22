using System.Linq.Expressions;
using GestioneSagre.EFCore.GenericRepository.Interfaces;
using GestioneSagre.EFCore.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GestioneSagre.EFCore.GenericRepository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    public DbContext DbContext { get; }

    public Repository(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<TEntity>> GetItemsAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (includes != null)
        {
            query = includes(query);
        }

        if (condition != null)
        {
            query = query.Where(condition);
        }

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetOrderedItemsAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> conditionWhere, Expression<Func<TEntity, dynamic>> orderBy,
        OrderType orderType = OrderType.Ascending, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (includes != null)
        {
            query = includes(query);
        }

        if (conditionWhere != null)
        {
            query = query.Where(conditionWhere);
        }

        if (orderBy != null)
        {
            if (orderType == OrderType.Ascending)
            {
                query = query.OrderBy(orderBy);
            }

            if (orderType == OrderType.Descending)
            {
                query = query.OrderByDescending(orderBy);
            }
        }

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetItemByIdAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (includes != null)
        {
            query = includes(query);
        }

        if (condition != null)
        {
            query = query.Where(condition);
        }

        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetItemsCountAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (includes != null)
        {
            query = includes(query);
        }

        if (condition != null)
        {
            query = query.Where(condition);
        }

        return await query
            .AsNoTracking()
            .CountAsync(cancellationToken);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        using var transaction = DbContext.Database.BeginTransaction();

        try
        {
            DbContext.Set<TEntity>().Add(entity);

            await DbContext.SaveChangesAsync(cancellationToken);

            DbContext.Entry(entity).State = EntityState.Detached;

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        using var transaction = DbContext.Database.BeginTransaction();

        try
        {
            DbContext.Set<TEntity>().Update(entity);

            await DbContext.SaveChangesAsync(cancellationToken);

            DbContext.Entry(entity).State = EntityState.Detached;

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        using var transaction = DbContext.Database.BeginTransaction();

        try
        {
            DbContext.Set<TEntity>().Remove(entity);

            await DbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}