using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class DVar : INameable, IDescribable
{
    public int ModelId { get; init; }

    [ForeignKey(nameof(ModelId))]
    public required Model Model { get; init; }

    public int ColIndex { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public override bool Equals(object? obj) => obj is DVar dvar
        && dvar.ModelId == ModelId
        && dvar.ColIndex == ColIndex;

    public override int GetHashCode() => HashCode.Combine(ModelId, ColIndex);
}
