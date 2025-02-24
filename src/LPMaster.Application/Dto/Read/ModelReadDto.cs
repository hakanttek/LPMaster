using LPMaster.Application.Dto.Update;
using LPMaster.Domain.Enums;

namespace LPMaster.Application.Dto.Read;

public record ModelReadDto
{
    public required int Id { get; init; }

    public required Objective Objective { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
