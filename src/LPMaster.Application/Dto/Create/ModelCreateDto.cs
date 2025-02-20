using LPMaster.Domain.Enums;

namespace LPMaster.Application.Dto.Create;

public record ModelCreateDto
{
    public required Objective Objective { get; set; }

    public ExpressionCreateDto ObjectiveFunction { get; init; } = new ExpressionCreateDto();

    protected internal List<EquationCreateDto> _constraints = new();

    public IEnumerable<EquationCreateDto> Constraints
    {
        get => _constraints;
        init => _constraints = value.ToList();
    }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
