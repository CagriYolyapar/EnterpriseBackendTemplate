using EnterpriseBackendTemplate.Application.Common;
using EnterpriseBackendTemplate.Application.Features.Products.GetById;
using EnterpriseBackendTemplate.Contract.PersistenceContracts;
using EnterpriseBackendTemplate.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.GetAll;

internal sealed class GetProductsQueryHandler(IRepository<Product> repository) : IRequestHandler<GetProductsQuery, Result<PagedResult<ProductResponse>>>
{
    public async Task<Result<PagedResult<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await repository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

        var totalCount = await repository.CountAsync(cancellationToken);

        var items = products.Select(product => new ProductResponse(product.Id, product.Name, product.Price)).ToList();

        var response = new PagedResult<ProductResponse>(items, request.PageNumber, request.PageSize, totalCount);
        return Result<PagedResult<ProductResponse>>.Success(response);
    }
}
