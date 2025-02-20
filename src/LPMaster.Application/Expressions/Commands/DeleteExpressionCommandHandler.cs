using MediatR;

namespace LPMaster.Application.Expressions.Commands;

public record DeleteExpressionsCommand(int? Id= null, string? Name = null) : IRequest;

public class DeleteExpressionCommandHandler : IRequestHandler<DeleteExpressionsCommand>
{
    public Task Handle(DeleteExpressionsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
