using LPMaster.Domain.Entities.Base;

namespace LPMaster.Domain.Entities;

public class DVar : INameable, IDescribable
{
    public int Id { get; set; }

    public int ModelId { get; init; }

    public int ColIndex { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
