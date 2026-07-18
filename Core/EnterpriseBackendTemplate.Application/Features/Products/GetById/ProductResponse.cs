using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.GetById;

public sealed record ProductResponse(Guid Id, string Name, decimal Price);


