using LPMaster.Domain.Enums;

namespace LPMaster.Application.Common.Dto.Read;

public record ModelReadDto
{
    public required int Id { get; init; }

    public required Objective Objective { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
