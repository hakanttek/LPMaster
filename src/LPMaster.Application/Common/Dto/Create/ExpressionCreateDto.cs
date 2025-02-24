namespace LPMaster.Application.Common.Dto.Create;

public record ExpressionCreateDto()
{
    public IEnumerable<MultiCreateDto> Multis { get; init; } = Enumerable.Empty<MultiCreateDto>();

    public string? Description { get; init; }

    public int ModelId { get; internal set; }
}
