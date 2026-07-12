using EnterpriseBackendTemplate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Contract.PersistenceContracts;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Guid id,CancellationToken cancellationToken = default);
    void Add(T entity);
    void Remove(T entity);
}

