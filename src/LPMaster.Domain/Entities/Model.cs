using LPMaster.Domain.Entities.Base;
using LPMaster.Domain.Enums;

namespace LPMaster.Domain.Entities;

public class Model : IHasId<int>, INameable, IDescribable, IVerifiable
{
    public required int Id { get; init; }

    public required Objective Objective { get; init; }

    public Expression? ObjectiveFunction { get; init; }

    public IEnumerable<Equation> Constraints { get; init; } = Enumerable.Empty<Equation>();

    public required string Name { get; init; }

    public string? Description { get; init; }

    public bool Verified => ObjectiveFunction?.Verified ?? false && Constraints.All(c => c.Verified);

    public IEnumerable<DVar> DVars { get; init; } = Enumerable.Empty<DVar>();
}
