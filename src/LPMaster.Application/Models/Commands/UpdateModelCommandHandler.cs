using AutoMapper;
using LPMaster.Application.Common.Contracts.Repositories;
using LPMaster.Application.Common.Contracts.Repositories.Base;
using LPMaster.Application.Common.Models.Update;
using LPMaster.Application.Common.Exceptions;
using MediatR;
using LPMaster.Application.Common.Models;

namespace LPMaster.Application.Models.Commands;

public record UpdateModelCommand(ModelUpdateDto Model) : ModelLookupDto, IRequest;

public class UpdateModelCommandHandler(IModelRepository repository, IMapper mapper) : IRequestHandler<UpdateModelCommand>
{
    public async Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        var model = (request.Id is null
                ? await repository.ReadFirstAsync(m => m.Name == request.Name, true, cancellationToken)
                : await repository.ReadFirstAsync(m => m.Id == request.Id, true, cancellationToken))
                ?? throw new NotFoundException("Model not found");

        mapper.Map(request.Model, model);

        await repository.UpdateAsync(model, cancellationToken);
    }
}
