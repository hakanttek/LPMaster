using FluentValidation;
using LPMaster.Application.Common.Exceptions;
using MediatR;

namespace LPMaster.Application.Common.Behaviours;
public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validator) : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                validator.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
            {
                var validationException = new ValidationException(failures);
                throw new BadRequestException("One or more validation errors occurred.", validationException);
            }
        }
        return await next();
    }
}
