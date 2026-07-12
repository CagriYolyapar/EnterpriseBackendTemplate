using EnterpriseBackendTemplate.Contract.PersistenceContracts;
using EnterpriseBackendTemplate.Domain.Common;
using Microsoft.EntityFrameworkCore;


namespace EnterpriseBackendTemplate.Persistence.Context;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}

