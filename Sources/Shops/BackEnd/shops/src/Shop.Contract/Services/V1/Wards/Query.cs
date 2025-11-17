using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Wards;
public static class Query
{
    public record GetWardsQuery(string? SearchTerm, int? ProvincyId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WardResponse>>;
    public record GetWardByIdQuery(int ID) : IQuery<Response.WardDetailResponse>;
    public record GetWardsByProvincyQuery(int ProvincyId) : IQuery<IList<Response.WardResponse>>;
}
