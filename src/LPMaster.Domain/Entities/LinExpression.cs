using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class LinExpression : IHasId<int>, IDescribable, IVerifiable
{
    public required int Id { get; init; }

    public List<Multi> Multis { get; init; } = new List<Multi>();

    public string? Description { get; init; }

    public int ModelId { get; init; }

    [ForeignKey(nameof(ModelId))]
    public required LinModel Model { get; init; }

    public bool Constant => !Multis.Any() || Multis.Any(m => !m.Constant);

    public bool Verified => ModelId == Model.Id && Multis.Any() && Multis.All(m => m.Model.Id == ModelId);
}
