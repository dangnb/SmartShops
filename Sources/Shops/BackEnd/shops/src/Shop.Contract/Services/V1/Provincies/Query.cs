using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Provincies;
public static class Query
{
    public record GetProvinciesQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Response.ProvincyResponse>>;
    public record GetProvincyByIdQuery(Guid Id) : IQuery<Response.ProvincyResponse>;
    public record GetByCompanyQuery() : IQuery<IList<Response.ProvincyResponse>>;
}
