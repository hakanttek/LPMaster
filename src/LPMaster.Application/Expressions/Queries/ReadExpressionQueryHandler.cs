using LPMaster.Application.Common.Models.Read;
using MediatR;

namespace LPMaster.Application.Expressions.Queries;

public record ReadExpressionQuery(int? ModelId = null, string? ModelName = null) : IRequest<IEnumerable<ExpressionReadDto>>;

public class ReadExpressionQueryHandler : IRequestHandler<ReadExpressionQuery, IEnumerable<ExpressionReadDto>>
{
    public Task<IEnumerable<ExpressionReadDto>> Handle(ReadExpressionQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
