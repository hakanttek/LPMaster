using LPMaster.Domain.Enums;

namespace LPMaster.Application.Common.Models.Update;

public record ModelUpdateDto
{
    public required Objective Objective { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
