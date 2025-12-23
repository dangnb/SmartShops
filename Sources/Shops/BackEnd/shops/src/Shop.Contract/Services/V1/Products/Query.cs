using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Products;
public static class Query
{
    public record GetProductsQuery(string? SearchTerm, Guid? CategoryId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.ProductResponse>>;
    public record GetProductByIdQuery(Guid Id) : IQuery<Response.ProductResponse>;
}
