using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Application.Dto.Create;

public record ModelCreateDto
{
    public required Objective Objective { get; set; }

    public ExpressionCreateDto ObjectiveFunction { get; init; } = new ExpressionCreateDto();

    protected internal List<Equation> _constraints = new List<Equation>();

    public IEnumerable<Equation> Constraints { get => _constraints; init => _constraints = value.ToList(); }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
