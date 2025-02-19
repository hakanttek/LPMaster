namespace LPMaster.Application.Dto.Create;

public record DVarCreateDto()
{
    public int ModelId { get; internal set; } = -1;

    public int ColIndex { get; internal set; } = -1;

    public string? Name { get; init; }

    public string? Description { get; init; }

    public bool BoundToModel => ModelId >= 0 && ColIndex >= 0;
}
