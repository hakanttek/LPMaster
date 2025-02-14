using LPMaster.Domain.Entities.Base;

namespace LPMaster.Domain.Entities;

public class Expression : IDescribable
{
    public int Id { get; init; }

    public required IEnumerable<Multi> Multis { get; init; }

    public string? Description { get; init; }
}
