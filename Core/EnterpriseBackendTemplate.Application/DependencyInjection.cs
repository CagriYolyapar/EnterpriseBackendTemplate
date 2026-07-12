using EnterpriseBackendTemplate.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;



namespace EnterpriseBackendTemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        return services;
    }
}

