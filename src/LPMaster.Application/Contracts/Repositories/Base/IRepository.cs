using System.Linq.Expressions;

namespace LPMaster.Application.Contracts.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>>? filter, bool tracked = false, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);   

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    #region Range
    Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    #endregion
}
