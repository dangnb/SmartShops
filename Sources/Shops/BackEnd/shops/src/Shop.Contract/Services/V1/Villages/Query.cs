using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Villages;
public static class Query
{
    public record GetVillagesQuery(string? SearchTerm, int? WardId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.VillageResponse>>;
    public record GetVillageByIdQuery(int ID) : IQuery<Response.VillageDetailResponse>;
    public record GetVillagesByWardIdQuery(int WardId) : IQuery<IList<Response.VillageResponse>>;
}
