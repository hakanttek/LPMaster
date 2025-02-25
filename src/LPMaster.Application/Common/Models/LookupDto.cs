using LPMaster.Application.Common.Exceptions;

namespace LPMaster.Application.Common.Models;

public record LookupDto
{
    public int? Id { get; init; } = null;
    public string? Name { get; init; } = null;

    public void Foo()
    {
        // Check if either Id or Name is provided
        if (Id is null && Name is null)
            throw new BadRequestException("Id or Name must be provided");
        // Check if both Id and Name are provided
        else if (Id is not null && Name is not null)
            throw new BadRequestException("Only one of Id or Name must be provided");
    }
}