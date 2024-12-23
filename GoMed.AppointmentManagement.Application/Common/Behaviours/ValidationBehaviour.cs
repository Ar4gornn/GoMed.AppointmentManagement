using GoMed.AppointmentManagement.Application.Common.Models;
using FluentValidation;
using MediatR;
using ValidationException = GoMed.AppointmentManagement.Application.Common.Exceptions.ValidationException;

namespace GoMed.AppointmentManagement.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        // Perform validation
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        // Collect validation failures
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(f => f.ErrorMessage).ToArray()
            );

        // If there are validation errors, return a validation result
        if (failures.Any())
        {
            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var validationErrorMethod = typeof(Result<>).MakeGenericType(resultType).GetMethod(
                    nameof(Result.ValidationError),
                    [typeof(Dictionary<string, string[]>)]);
                if (validationErrorMethod != null)
                {
                    return (TResponse)validationErrorMethod.Invoke(null, new object[] { failures })!;
                }
            }
            else if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.ValidationError(failures);
            }
            else
            {
                throw new ValidationException(failures);
            }
        }

        // If validation passes, continue to next pipeline stage
        return await next();
    }
}