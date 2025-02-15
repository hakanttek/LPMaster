using LPMaster.Domain.Entities.Base;

namespace LPMaster.Domain.Entities;

public class Expression : IUnique<int>, IDescribable
{
    public int Id { get; init; }

    public required IEnumerable<Multi> Multis { get; init; }

    public string? Description { get; init; }

    public Model? Model => Multis
        .Where(multi => multi.ModelId is not null)
        .Select(multi => multi.Model)
        .Distinct()
        .SingleOrDefault();

    public int? ModelId => Model?.Id;
}
