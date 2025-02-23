using LPMaster.Domain.Entities.Base;
using LPMaster.Domain.Enums;

namespace LPMaster.Domain.Entities;

public class LinModel : IHasId<int>, INameable, IDescribable, IVerifiable
{
    public required int Id { get; init; }

    public required Objective Objective { get; init; }

    public LinExpression? ObjectiveFunction { get; init; }

    public IEnumerable<LinEquation>? Constraints { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }

    public bool Verified => ObjectiveFunction.IsVerified() && Constraints.IsVerified();

    public IEnumerable<DVar> DVars { get; init; } = Enumerable.Empty<DVar>();
}
