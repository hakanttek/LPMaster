using LPMaster.Application.Dto.Update;
using MediatR;

namespace LPMaster.Application.Expressions.Commands;

public record UpdateModelCommand(ModelUpdateDto Model, int? Id = null, string? Name = null) : IRequest;

public class UpdateExpressionCommandHandler : IRequestHandler<UpdateModelCommand>
{
    public Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
