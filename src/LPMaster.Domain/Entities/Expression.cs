using LPMaster.Domain.Entities.Base;

namespace LPMaster.Domain.Entities;

public class Expression : IUnique<int>, IDescribable, IVerifiable
{
    public int Id { get; init; }

    public required IEnumerable<Multi> Multis { get; init; }

    public string? Description { get; init; }

    public Model Model => Multis
        .Where(multi => multi.ModelId is not null)
        .Select(multi => multi.Model)
        .Distinct()
        .SingleOrDefault() ?? throw new InvalidOperationException("The multis of the expression either have no model or have different models.");

    public int? ModelId => Model?.Id;

    public bool Verified => Multis
        .Where(multi => multi.ModelId is not null)
        .Select(multi => multi.Model)
        .Distinct()
        .Count() == 1;
}
