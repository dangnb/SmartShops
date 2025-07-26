using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Wards;
public static class Query
{
    public record GetWardsQuery(string? SearchTerm, int? DistrictId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.WardResponse>>;
    public record GetWardByIdQuery(int ID) : IQuery<Response.WardDetailResponse>;
    public record GetWardByDistrictQuery(int DistrictId) : IQuery<IList<Response.WardResponse>>;
}
