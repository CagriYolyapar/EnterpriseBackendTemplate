using EnterpriseBackendTemplate.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.Create;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<Result<Guid>>;



