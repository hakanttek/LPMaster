using LPMaster.Application.Common.Contracts.Repositories;
using LPMaster.Application.Common.Contracts.Repositories.Base;
using LPMaster.Application.Common.Exceptions;
using LPMaster.Application.Common.Models;
using MediatR;

namespace LPMaster.Application.Models.Commands;

public record DeleteModelCommand : ModelLookupDto, IRequest;

public class DeleteModelCommandHandler(IModelRepository repository) : IRequestHandler<DeleteModelCommand>
{
    public async Task Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        var model = (request.Id is null
                ? await repository.ReadFirstAsync(m => m.Name == request.Name, true, cancellationToken)
                : await repository.ReadFirstAsync(m => m.Id == request.Id, true, cancellationToken))
                ?? throw new NotFoundException("Model not found");

        await repository.DeleteAsync(model, cancellationToken);
    }
}
