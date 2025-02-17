﻿using LPMaster.Domain.Entities.Base;

namespace LPMaster.Solver.Contracts.Repositories.Base;

public interface INamedEntityRepository<TEntity> : IRepository<TEntity> where TEntity : class, INameable
{
    Task<TEntity> ReadByNameAsync(string? name);
}
