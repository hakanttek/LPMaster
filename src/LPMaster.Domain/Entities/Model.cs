using LPMaster.Domain.Entities.Base;

namespace LPMaster.Domain.Entities;

public class Model : IUnique<int>, INameable, IDescribable
{
    public int Id { get; init; }

    public int Object { get; init; }

    public required Expression ObjectiveFunction { get; init; }

    public required IEnumerable<Equation> Constraints { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
