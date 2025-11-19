using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Suppliers;
public static class Query
{
    public record GetSuppliersQuery(string? SearchTerm, Guid? ProvinceId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.SupplierResponse>>;
    public record GetSupplierByIdQuery(Guid Id) : IQuery<Response.SupplierDetailResponse>;
}
