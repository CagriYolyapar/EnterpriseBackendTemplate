using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EnterpriseBackendTemplate.Application.Common.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IValidator<TRequest>[] _validators = validators.ToArray();
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Length == 0)
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);

        //var validationResults = await Task.WhenAll(
        //    validators.Select(validator =>
        //        validator.ValidateAsync(
        //            context,
        //            cancellationToken)));

        var validationResults = new List<ValidationResult>(
      _validators.Length);

        foreach (var validator in _validators)
        {
            validationResults.Add(
                await validator.ValidateAsync(
                    context,
                    cancellationToken));
        }

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToArray();

        if (failures.Length > 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}

