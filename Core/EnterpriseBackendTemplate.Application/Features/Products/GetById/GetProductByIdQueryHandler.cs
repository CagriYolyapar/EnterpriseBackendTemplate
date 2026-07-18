using EnterpriseBackendTemplate.Application.Common;
using EnterpriseBackendTemplate.Contract.PersistenceContracts;
using EnterpriseBackendTemplate.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.GetById;

internal sealed class GetProductByIdQueryHandler(IRepository<Product> repository) : IRequestHandler<GetProductByIdQuery, Result<ProductResponse>>
{
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(
          request.Id,
          cancellationToken);

        if (product is null)
        {
            return Result<ProductResponse>.Failure(
                "Product was not found.");
        }

        var response = new ProductResponse(
            product.Id,
            product.Name,
            product.Price);

        return Result<ProductResponse>.Success(response);
    }
}


