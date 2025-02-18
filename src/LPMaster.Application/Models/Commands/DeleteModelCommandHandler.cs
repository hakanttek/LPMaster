using MediatR;

namespace LPMaster.Application.Models.Commands;

public record DeleteModelCommand(int? Id= null, string? Name = null) : IRequest;

public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand>
{
    public Task Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
