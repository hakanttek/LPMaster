using System.Linq.Expressions;

namespace LPMaster.Application.Contracts.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The entity created</returns>
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>>? filter, bool tracked = false, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);   

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    #region Range
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The list of entities created</returns>
    Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    #endregion
}
