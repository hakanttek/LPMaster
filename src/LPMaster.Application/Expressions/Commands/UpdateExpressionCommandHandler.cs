using LPMaster.Application.Common.Models.Update;
using MediatR;

namespace LPMaster.Application.Expressions.Commands;

public record UpdateExpressionCommand(int Id) : ExpressionUpdateDto, IRequest;

public class UpdateExpressionCommandHandler : IRequestHandler<UpdateExpressionCommand>
{
    public Task Handle(UpdateExpressionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
