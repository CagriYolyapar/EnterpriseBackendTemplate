using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseBackendTemplate.WebApi.ExceptionHandling;

internal sealed class ValidationExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }


        logger.LogInformation(
    "Validation failed for {RequestPath}",
    httpContext.Request.Path);


        var errors = validationException.Errors
    .GroupBy(error =>
        string.IsNullOrWhiteSpace(error.PropertyName)
            ? "General"
            : error.PropertyName)
    .ToDictionary(
        group => group.Key,
        group => group
            .Select(error => error.ErrorMessage)
            .Distinct()
            .ToArray());

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        return await problemDetailsService.TryWriteAsync(
           new ProblemDetailsContext
           {
               HttpContext = httpContext,
               ProblemDetails = new ValidationProblemDetails(errors)
               {
                   Status = StatusCodes.Status400BadRequest,
                   Title = "One or more validation errors occurred.",
                   Type =
                       "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                   Instance = httpContext.Request.Path
               }
           });


    }
}

