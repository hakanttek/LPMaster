namespace LPMaster.Application.Dto.Update;

public record DVarUpdateDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}
