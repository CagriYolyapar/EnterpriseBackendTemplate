using EnterpriseBackendTemplate.Contract.PersistenceContracts;
using EnterpriseBackendTemplate.Persistence.Context;
using EnterpriseBackendTemplate.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString =
            configuration.GetConnectionString("SqlServer")
            ?? throw new InvalidOperationException(
                "Connection string 'SqlServer' was not found.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<AppDbContext>());

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}

