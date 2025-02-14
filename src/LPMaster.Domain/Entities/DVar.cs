using LPMaster.Domain.Entities.Base;

namespace LPMaster.Domain.Entities;

public class DVar : IUnique<int>, INameable, IDescribable
{
    public int Id { get; init; }

    public int ModelId { get; init; }

    public required Model Model { get; init; }

    public int ColIndex { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
