using LPMaster.Application.Common.Dto.Create;
using MediatR;

namespace LPMaster.Application.Expressions.Commands;

public record CreateExpressionCommand() : ExpressionCreateDto, IRequest<int>;

public class CreateExpressionCommandHandler : IRequestHandler<CreateExpressionCommand, int>
{
    public Task<int> Handle(CreateExpressionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
