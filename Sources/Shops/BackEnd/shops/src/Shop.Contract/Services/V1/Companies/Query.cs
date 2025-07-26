using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Enumerations;

namespace Shop.Contract.Services.V1.Companies;
public static class PermissionQuery
{
    public record GetPermissionsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.PermissionsResponse>>;
}
