using System.Linq.Expressions;

namespace LPMaster.Application.Common.Contracts.Repositories.Base;

public static class RepositoryExtensions
{
    public static async Task<TEntity?> ReadFirstAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, bool>>? filter, bool tracked = false, CancellationToken cancellationToken = default)
        where TEntity : class
        => (await repository.ReadAsync(filter, tracked, cancellationToken)).FirstOrDefault();

    public static async Task<TEntity?> ReadSingleAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, bool>>? filter, bool tracked = false, CancellationToken cancellationToken = default)
        where TEntity : class
        => (await repository.ReadAsync(filter, tracked, cancellationToken)).SingleOrDefault();

    public static async Task<bool> AnyAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
        where TEntity : class
        => 0 < await repository.CountAsync(filter, cancellationToken);
}
