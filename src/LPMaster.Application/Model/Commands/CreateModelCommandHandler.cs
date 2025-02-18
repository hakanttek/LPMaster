using MediatR;

namespace LPMaster.Application.Model.Commands;

public record CreateModelCommand : IRequest<int>;

public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, int>
{
    public Task<int> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
