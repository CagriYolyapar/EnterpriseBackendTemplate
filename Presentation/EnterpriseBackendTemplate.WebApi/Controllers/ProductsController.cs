using EnterpriseBackendTemplate.Application.Features.Products.Create;
using EnterpriseBackendTemplate.Application.Features.Products.GetAll;
using EnterpriseBackendTemplate.Application.Features.Products.GetById;
using EnterpriseBackendTemplate.Application.Features.Products.Update;
using EnterpriseBackendTemplate.WebApi.Contracts.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseBackendTemplate.WebApi.Controllers;

[Route("api/products")]
[ApiController]
public sealed class ProductsController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetProductsRequest request,CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery(request.PageNumber, request.PageSize);
        var result = await sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }



    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetProductByIdQuery(id),
            cancellationToken);

        return result.IsSuccess
          ? Ok(result.Value)
          : NotFound(new { error = result.Error });
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(request.Name, request.Price);

        var result = await sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                error = result.Error
            });
        }

        return CreatedAtAction(
    nameof(GetById),
    new { id = result.Value },
    result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
      id,
      request.Name,
      request.Price);

        var result = await sender.Send(
            command,
            cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : NotFound(new { error = result.Error });
    }
}

