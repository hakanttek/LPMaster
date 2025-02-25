using FluentValidation;

namespace LPMaster.Application.Models.Queries;

public class ReadModelQueryValidator : AbstractValidator<ReadModelQuery>
{
    public ReadModelQueryValidator()
    {
        RuleFor(x => x.Id)
            .Null().When(x => x.Name is not null)
            .WithMessage("Only one of Id or Name must be provided");
        RuleFor(x => x.Name)
            .Null().When(x => x.Id is not null)
            .WithMessage("Only one of Id or Name must be provided");
        RuleFor(x => x.Id)
            .NotNull().When(x => x.Name is null)
            .WithMessage("Id or Name must be provided");
        RuleFor(x => x.Name)
            .NotNull().When(x => x.Id is null)
            .WithMessage("Id or Name must be provided");
    }
}
