using LPMaster.Application.Dto.Read;
using MediatR;

namespace LPMaster.Application.Expressions.Queries;

public record ReadExpressionQuery(int? Id = null, int? ModelId = null) : IRequest<ExpressionReadDto>;

public class ReadExpressionQueryHandler : IRequestHandler<ReadExpressionQuery, ExpressionReadDto>
{
    public Task<ExpressionReadDto> Handle(ReadExpressionQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
