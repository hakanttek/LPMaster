using LPMaster.Application.Dto.Update;
using MediatR;

namespace LPMaster.Application.Models.Commands;

public record UpdateModelCommand(ModelUpdateDto Model, int? Id = null, string? Name = null) : IRequest;

public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand>
{
    public Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
