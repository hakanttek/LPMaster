using AutoMapper;
using LPMaster.Application.Common.Contracts.Repositories;
using LPMaster.Application.Common.Contracts.Repositories.Base;
using LPMaster.Application.Common.Models.Read;
using LPMaster.Application.Common.Exceptions;
using MediatR;

namespace LPMaster.Application.Models.Queries;

public record ReadModelQuery(int? Id = null, string? Name = null) : IRequest<ModelReadDto>;

public class ReadModelQueryHandler(IModelRepository repository, IMapper mapper) : IRequestHandler<ReadModelQuery, ModelReadDto>
{
    public async Task<ModelReadDto> Handle(ReadModelQuery request, CancellationToken cancellationToken)
    {
        // Check if either Id or Name is provided
        if (request.Id is null && request.Name is null)
            throw new BadRequestException("Id or Name must be provided");
        // Check if both Id and Name are provided
        else if (request.Id is not null && request.Name is not null)
            throw new BadRequestException("Only one of Id or Name must be provided");

        var model = (request.Id is null
                ? await repository.ReadFirstAsync(m => m.Name == request.Name, cancellationToken: cancellationToken)
                : await repository.ReadFirstAsync(m => m.Id == request.Id, cancellationToken: cancellationToken))
                ?? throw new NotFoundException("Model not found");

        return mapper.Map<ModelReadDto>(model);
    }
}
