using LPMaster.Application.Model.Dto;
using MediatR;

namespace LPMaster.Application.Model.Queries;

public record ReadModelQuery(int? Id = null, int? Name = null) : IRequest<ModelReadDto>;

public class ReadModelQueryHandler : IRequestHandler<ReadModelQuery, ModelReadDto>
{
    public Task<ModelReadDto> Handle(ReadModelQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
