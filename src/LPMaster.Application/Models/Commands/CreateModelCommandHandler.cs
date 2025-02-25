using AutoMapper;
using LPMaster.Application.Common.Contracts.Repositories;
using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;
using MediatR;

namespace LPMaster.Application.Models.Commands;

public record CreateModelCommand(string Name, Objective Objective = Objective.Minimization, string? Description = null) : IRequest<CreateModelResult>;

public record CreateModelResult(int Id, string Name);

public class CreateModelCommandHandler(IMapper mapper, IModelRepository repository) : IRequestHandler<CreateModelCommand, CreateModelResult>
{
    public async Task<CreateModelResult> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var model = mapper.Map<LinModel>(request);
        var createdModel = await repository.CreateAsync(model, cancellationToken);
        return new CreateModelResult(createdModel.Id, createdModel.Name);
    }
}
