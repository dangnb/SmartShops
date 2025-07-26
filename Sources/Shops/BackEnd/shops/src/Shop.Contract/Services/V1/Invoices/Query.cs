using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Invoices;
public static class Query
{
    public record GetInvoicesQuery(string? SearchTerm, int? CityId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.InvoiceResponse>>;
    public record GetInvoiceByIdQuery(int ID) : IQuery<Response.InvoiceResponse>;
}
