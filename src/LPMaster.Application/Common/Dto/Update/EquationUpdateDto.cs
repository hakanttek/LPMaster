using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Application.Common.Dto.Update;

public record EquationUpdateDto
{
    public LinExpression? LeftExpression { get; set; }

    public LinExpression? RightExpression { get; set; }

    public required Relation Relation { get; set; }

    public string? Description { get; set; }
}
