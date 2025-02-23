using LPMaster.Domain.Enums;

namespace LPMaster.Application.Dto.Create;

/// <summary>
/// Data transfer object for creating a model.
/// </summary>
public record ModelCreateDto()
{
    /// <summary>
    /// The objective of the model. Default is <see cref="Objective.Minimization"/>.
    /// </summary>
    public Objective Objective { get; init; } = Objective.Minimization;

    /// <summary>
    /// The name of the model.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The description of the model.
    /// </summary>
    public string? Description { get; init; } = null;
}
