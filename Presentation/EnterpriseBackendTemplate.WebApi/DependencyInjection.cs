using EnterpriseBackendTemplate.WebApi.ExceptionHandling;

namespace EnterpriseBackendTemplate.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
       this IServiceCollection services)
    {
        services.AddProblemDetails();

        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
