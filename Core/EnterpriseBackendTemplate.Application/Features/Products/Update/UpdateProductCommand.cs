using EnterpriseBackendTemplate.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.Update;

public sealed record UpdateProductCommand(Guid Id, string Name, decimal Price) : IRequest<Result>;

