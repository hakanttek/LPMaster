using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class DVar : INameable, IDescribable, IVerifiable
{
    public int ModelId { get; init; }

    [ForeignKey(nameof(ModelId))]
    public required Model Model { get; init; }

    public required int ColIndex { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public bool Verified => ModelId == Model.Id;
}
