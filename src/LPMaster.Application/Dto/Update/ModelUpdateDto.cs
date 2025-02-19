using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Application.Dto.Update;

public record ModelUpdateDto
{
    public required int Id { get; init; }

    public required Objective Objective { get; set; }

    public Expression? ObjectiveFunction { get; set; }

    public IEnumerable<Equation> Constraints { get; init; } = Enumerable.Empty<Equation>();

    public string? Name { get; set; }

    public string? Description { get; set; }

    public IEnumerable<DVar> DVars { get; init; } = Enumerable.Empty<DVar>();
}
