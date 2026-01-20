using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Purchasing.Warehouses;

public static class Query
{
    public record GetWarehousesQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WarehouseResponse>>;
    public record GeWarehouseByIdQuery(Guid Id) : IQuery<Response.WarehouseResponse>;
}
