using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Cities;
public static class Query
{
    public record GetCitiesQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Response.CityResponse>>;
    public record GetCityByIdQuery(int ID) : IQuery<Response.CityResponse>;
    public record GetByCompanyQuery() : IQuery<IList<Response.CityResponse>>;
}
