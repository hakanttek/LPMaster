using LPMaster.Domain.Entities;
using LPMaster.Application.Contracts.Repositories.Base;

namespace LPMaster.Application.Contracts.Repositories;

public interface IModelRepository : IRepository<Model>, INamedEntityRepository<Model>
{
}
