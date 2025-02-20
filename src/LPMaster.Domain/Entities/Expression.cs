using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class Expression : IHasId<int>, IDescribable, IVerifiable
{
    public required int Id { get; init; }

    public IEnumerable<Multi> Multis { get; init; } = Enumerable.Empty<Multi>();

    public string? Description { get; init; }

    public int ModelId { get; init; }

    [ForeignKey(nameof(ModelId))]
    public required Model Model { get; init; }

    public bool Verified => ModelId == Model.Id && Multis.All(m => m.Model.Id == ModelId);
}
