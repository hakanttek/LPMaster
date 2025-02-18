using LPMaster.Application.Dto.Read;
using MediatR;

namespace LPMaster.Application.Models.Queries;

public record ReadModelQuery(int? Id = null, int? Name = null) : IRequest<ModelReadDto>;

public class ReadModelQueryHandler : IRequestHandler<ReadModelQuery, ModelReadDto>
{
    public Task<ModelReadDto> Handle(ReadModelQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
