using EnterpriseBackendTemplate.Application.Features.Products.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseBackendTemplate.WebApi.Controllers;

[Route("api/products")]
[ApiController]
public sealed class ProductsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return StatusCode(StatusCodes.Status201Created, new { id = result.Value });
    }
}

