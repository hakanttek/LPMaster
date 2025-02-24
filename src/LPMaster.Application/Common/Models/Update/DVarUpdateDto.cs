namespace LPMaster.Application.Common.Models.Update;

public record DVarUpdateDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}
