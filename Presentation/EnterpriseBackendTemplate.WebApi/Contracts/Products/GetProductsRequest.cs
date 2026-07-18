namespace EnterpriseBackendTemplate.WebApi.Contracts.Products;

public sealed record GetProductsRequest
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
