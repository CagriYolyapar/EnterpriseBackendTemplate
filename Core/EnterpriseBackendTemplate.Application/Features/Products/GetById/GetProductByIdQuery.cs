using EnterpriseBackendTemplate.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.GetById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductResponse>>;



