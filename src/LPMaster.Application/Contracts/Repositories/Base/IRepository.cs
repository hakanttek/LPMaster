namespace LPMaster.Application.Contracts.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    Task CreateAsync(TEntity entity);
    
    Task<IEnumerable<TEntity>> ReadAllAsync();

    Task UpdateAsync(TEntity entity);   

    Task DeleteAsync(TEntity entity);

    #region Range
    Task CreateAsync(IEnumerable<TEntity> entities);

    Task UpdateAsync(IEnumerable<TEntity> entities);

    Task DeleteAsync(IEnumerable<TEntity> entities);
    #endregion
}
