using FluentValidation;
using LPMaster.Application.Common.Contracts.Repositories;
using LPMaster.Application.Common.Contracts.Repositories.Base;

namespace LPMaster.Application.Models.Commands;

public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
{
    public CreateModelCommandValidator(IModelRepository repository)
    {
        RuleFor(model => model.Name)
            .MustAsync(async (name, cancellationToken) => !await repository.AnyAsync(m => m.Name == name, cancellationToken));
    }
}
