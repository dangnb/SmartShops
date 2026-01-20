using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Common.Provincies;
public static class Query
{
    public record GetProvincesQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Response.ProvinceResponse>>;
    public record GetProvinceByIdQuery(Guid Id) : IQuery<Response.ProvinceResponse>;
    public record GetByCompanyQuery() : IQuery<IList<Response.ProvinceResponse>>;
}
