using LPMaster.Application.Contracts.Repositories;
using LPMaster.Application.Contracts.Repositories.Base;
using LPMaster.Application.Exceptions;
using MediatR;

namespace LPMaster.Application.Models.Commands;

public record DeleteModelCommand(int? Id= null, string? Name = null) : IRequest;

public class DeleteModelCommandHandler(IModelRepository repository) : IRequestHandler<DeleteModelCommand>
{
    public async Task Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        // Check if either Id or Name is provided
        if (request.Id is null && request.Name is null)
            throw new BadRequestException("Id or Name must be provided");
        // Check if both Id and Name are provided
        else if (request.Id is not null && request.Name is not null)
            throw new BadRequestException("Only one of Id or Name must be provided");

        var model = (request.Id is null
                ? await repository.ReadFirstAsync(m => m.Name == request.Name, tracked: true, cancellationToken: cancellationToken)
                : await repository.ReadFirstAsync(m => m.Id == request.Id, tracked: true, cancellationToken: cancellationToken))
                ?? throw new NotFoundException("Model not found");

        await repository.DeleteAsync(model, cancellationToken);
    }
}
