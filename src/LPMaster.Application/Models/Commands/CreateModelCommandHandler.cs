using LPMaster.Application.Dto.Create;
using MediatR;

namespace LPMaster.Application.Models.Commands;

public record CreateModelCommand(ModelCreateDto Model) : IRequest<int>;

public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, int>
{
    public Task<int> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
