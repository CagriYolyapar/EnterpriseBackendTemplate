using EnterpriseBackendTemplate.Application.Common;
using EnterpriseBackendTemplate.Application.Features.Products.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.GetAll;

public sealed record GetProductsQuery(int PageNumber, int PageSize) : IRequest<Result<PagedResult<ProductResponse>>>;


