using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Common.Configs;
public static class Query
{
    public record GetConfigsQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Response.ConfigResponse>>;
    public record GetConfigByIdQuery(int ID) : IQuery<Response.ConfigResponse>;
}
