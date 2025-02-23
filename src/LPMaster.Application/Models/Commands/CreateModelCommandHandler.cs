using AutoMapper;
using LPMaster.Application.Contracts.Repositories;
using LPMaster.Application.Dto.Create;
using LPMaster.Domain.Entities;
using MediatR;

namespace LPMaster.Application.Models.Commands;

public record CreateModelCommand(): ModelCreateDto, IRequest<CreateModelResult>;

public record CreateModelResult(int Id, string? Name = null);

public class CreateModelCommandHandler(IMapper mapper, IModelRepository repository) : IRequestHandler<CreateModelCommand, CreateModelResult>
{
    public async Task<CreateModelResult> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var model = mapper.Map<LinModel>(request);
        var createdModel = await repository.CreateAsync(model, cancellationToken);
        return new CreateModelResult(createdModel.Id, createdModel.Name);
    }
}
