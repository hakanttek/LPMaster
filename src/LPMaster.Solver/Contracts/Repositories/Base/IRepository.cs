namespace LPMaster.Solver.Contracts.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    Task CreateAsync(TEntity entity);

    Task<IEnumerable<TEntity>> ReadAllAsync();

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}
