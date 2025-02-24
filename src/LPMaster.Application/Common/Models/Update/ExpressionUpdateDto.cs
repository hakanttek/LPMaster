namespace LPMaster.Application.Common.Models.Update;

public record ExpressionUpdateDto
{
    public IEnumerable<MultiUpdateDto> Multis { get; init; } = Enumerable.Empty<MultiUpdateDto>();

    public string? Description { get; init; }
}
