using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Application.Dto.Update;

public record ModelUpdateDto
{
    public required int Id { get; init; }

    public required Objective Objective { get; set; }

    public LinExpression? ObjectiveFunction { get; set; }

    public IEnumerable<LinEquation> Constraints { get; init; } = Enumerable.Empty<LinEquation>();

    public string? Name { get; set; }

    public string? Description { get; set; }

    public IEnumerable<DVar> DVars { get; init; } = Enumerable.Empty<DVar>();
}
