using System.Linq.Expressions;
using GestioneSagre.EFCore.Models.Enums;
using Microsoft.EntityFrameworkCore.Query;

namespace GestioneSagre.EFCore.GenericRepository.Interfaces;

public interface IRepository<TEntity> where TEntity : class, new()
{
    Task<List<TEntity>> GetItemsAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetOrderedItemsAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> conditionWhere, Expression<Func<TEntity, dynamic>> orderBy,
        OrderType orderType = OrderType.Ascending, CancellationToken cancellationToken = default);

    Task<TEntity> GetItemByIdAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    Task<int> GetItemsCountAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
        Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}