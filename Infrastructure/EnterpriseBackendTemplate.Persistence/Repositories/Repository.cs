using EnterpriseBackendTemplate.Contract.PersistenceContracts;
using EnterpriseBackendTemplate.Domain.Common;
using EnterpriseBackendTemplate.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Persistence.Repositories
{
    internal sealed class Repository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public void Add(T entity)
        {
             _dbSet.Add(entity);
        }

        public Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _dbSet.AnyAsync(
          entity => entity.Id == id,
          cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().CountAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber,int pageSize,CancellationToken cancellationToken = default)
        {
            return await context
                .Set<T>()
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public  Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return  _dbSet.FindAsync([id], cancellationToken).AsTask();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
