using LPMaster.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPMaster.Domain.Entities;

public class DVar : IUnique<int>, INameable, IDescribable, IVerifiable
{
    public int Id { get; init; }

    public int ModelId { get; init; }

    [ForeignKey(nameof(ModelId))]
    public required Model Model { get; init; }

    public required int ColIndex { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public bool Verified => ModelId == Model.Id;
}
