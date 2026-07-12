using EnterpriseBackendTemplate.Application.Common;
using EnterpriseBackendTemplate.Contract.PersistenceContracts;
using EnterpriseBackendTemplate.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.Create;

internal sealed class CreateProductCommandHandler(IRepository<Product> repository, IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price);
        repository.Add(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(product.Id);
    }
}

