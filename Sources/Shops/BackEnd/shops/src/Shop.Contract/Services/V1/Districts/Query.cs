using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Districts;
public static class Query
{
    public record GetDistrictsQuery(string? SearchTerm, int? CityId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.DistrictResponse>>;
    public record GetDistrictByIdQuery(int ID) : IQuery<Response.DistrictResponse>;
    public record GetDistrictsByCityQuery(int cityId) : IQuery<IList<Response.DistrictResponse>>;
    public record GetByUserQuery() : IQuery<IList<Response.DistrictResponse>>;
}
