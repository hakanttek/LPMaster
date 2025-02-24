using AutoMapper;
using LPMaster.Application.Common.Contracts.Repositories;
using LPMaster.Application.Common.Contracts.Repositories.Base;
using LPMaster.Application.Common.Models.Update;
using LPMaster.Application.Common.Exceptions;
using MediatR;

namespace LPMaster.Application.Models.Commands;

public record UpdateModelCommand(ModelUpdateDto Model, int? Id = null, string? Name = null) : IRequest;

public class UpdateModelCommandHandler(IModelRepository repository, IMapper mapper) : IRequestHandler<UpdateModelCommand>
{
    public async Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        // Check if either Id or Name is provided
        if (request.Id is null && request.Name is null)
            throw new BadRequestException("Id or Name must be provided");
        // Check if both Id and Name are provided
        else if (request.Id is not null && request.Name is not null)
            throw new BadRequestException("Only one of Id or Name must be provided");

        var model = (request.Id is null
                ? await repository.ReadFirstAsync(m => m.Name == request.Name, true, cancellationToken)
                : await repository.ReadFirstAsync(m => m.Id == request.Id, true, cancellationToken))
                ?? throw new NotFoundException("Model not found");

        mapper.Map(request.Model, model);

        await repository.UpdateAsync(model, cancellationToken);
    }
}
