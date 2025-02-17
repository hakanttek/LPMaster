using LPMaster.Domain.Entities;
using LPMaster.Solver.Contracts.Repositories.Base;

namespace LPMaster.Solver.Contracts.Repositories;

public interface IModelRepository : IRepository<Model>, INamedEntityRepository<Model>
{
}
