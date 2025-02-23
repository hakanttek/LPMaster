using AutoMapper;
using LPMaster.Application.Contracts.Repositories;
using LPMaster.Application.Contracts.Repositories.Base;
using LPMaster.Application.Dto.Read;
using MediatR;

namespace LPMaster.Application.Models.Queries;

public record ReadModelQuery(int? Id = null, string? Name = null) : IRequest<ModelReadDto>;

public class ReadModelQueryHandler(IModelRepository repository, IMapper mapper) : IRequestHandler<ReadModelQuery, ModelReadDto>
{
    public async Task<ModelReadDto> Handle(ReadModelQuery request, CancellationToken cancellationToken)
    {
        var model = request.Id is null
            ? await repository.ReadFirstAsync(m => m.Name == request.Name, cancellationToken: cancellationToken)
            : await repository.ReadFirstAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);

        return mapper.Map<ModelReadDto>(model);
    }
}
