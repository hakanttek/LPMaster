using LPMaster.Application.Model.Dto;
using MediatR;

namespace LPMaster.Application.Model.Commands;

public record UpdateModelCommand(int? Id = null, string? Name = null, ModelUpdateDto Update) : IRequest;

public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand>
{
    public Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
