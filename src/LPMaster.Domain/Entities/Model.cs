using LPMaster.Domain.Entities.Base;
using LPMaster.Domain.Enums;

namespace LPMaster.Domain.Entities;

public class Model : IHasId<int>, INameable, IDescribable
{
    public required int Id { get; init; }

    public required Objective Objective { get; init; }

    public required Expression ObjectiveFunction { get; init; }

    public required IEnumerable<Equation> Constraints { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
