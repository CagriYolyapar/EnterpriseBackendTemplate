using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseBackendTemplate.WebApi.ExceptionHandling;

internal sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
          exception,
          "An unhandled exception occurred while processing {RequestPath}",
          httpContext.Request.Path);

        httpContext.Response.StatusCode =
           StatusCodes.Status500InternalServerError;

        return await problemDetailsService.TryWriteAsync(
          new ProblemDetailsContext
          {
              HttpContext = httpContext,
              ProblemDetails = new ProblemDetails
              {
                  Status =
                      StatusCodes.Status500InternalServerError,
                  Title = "An unexpected error occurred.",
                  Type =
                      "https://tools.ietf.org/html/rfc9110#section-15.6.1",
                  Instance = httpContext.Request.Path
              }
          });
    }
}

