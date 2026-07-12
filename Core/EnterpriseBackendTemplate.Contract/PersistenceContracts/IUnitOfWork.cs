using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Contract.PersistenceContracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default);
}

