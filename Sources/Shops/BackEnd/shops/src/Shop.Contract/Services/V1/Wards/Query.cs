using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Wards;
public static class Query
{
    public record GetWardsQuery(string? SearchTerm, Guid? ProvincyId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WardResponse>>;
    public record GetWardByIdQuery(Guid Id) : IQuery<Response.WardDetailResponse>;
    public record GetWardsByProvincyQuery(Guid ProvincyId) : IQuery<IList<Response.WardResponse>>;
}
