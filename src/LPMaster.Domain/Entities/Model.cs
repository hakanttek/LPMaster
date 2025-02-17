using LPMaster.Domain.Entities.Base;
using LPMaster.Domain.Enums;
using System.Linq;

namespace LPMaster.Domain.Entities;

public class Model : IHasId<int>, INameable, IDescribable, IVerifiable
{
    public required int Id { get; init; }

    public required Objective Objective { get; init; }

    public required Expression ObjectiveFunction { get; init; }

    public required IEnumerable<Equation> Constraints { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public bool Verified => ObjectiveFunction.Verified && Constraints.All(c => c.Verified);
}
