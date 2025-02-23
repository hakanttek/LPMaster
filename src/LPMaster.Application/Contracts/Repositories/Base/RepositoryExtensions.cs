using System.Linq.Expressions;

namespace LPMaster.Application.Contracts.Repositories.Base;

public static class RepositoryExtensions
{
    public static async Task<TEntity?> ReadFirstAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, bool>>? filter, bool tracked = false, CancellationToken cancellationToken = default) where TEntity : class
        => (await repository.ReadAsync(filter, tracked, cancellationToken)).FirstOrDefault();

    public static async Task<TEntity?> ReadSingleAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, bool>>? filter, bool tracked = false, CancellationToken cancellationToken = default) where TEntity : class
        => (await repository.ReadAsync(filter, tracked, cancellationToken)).SingleOrDefault();
}
