using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Payments;
public static class Query
{
    public record GetPaymentsQuery(string? SearchTerm, int? WardId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.PaymentResponse>>;
    public record GetPaymentByIdQuery(Guid ID) : IQuery<Response.PaymentResponse>;
}
