using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Enumerations;

namespace Shop.Contract.Services.V1.Permissions;
public static class Query
{
    public record GetPermissionsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<PagedResult<Response.PermissionResponse>>;
    public record GetPermissionByIdQuery(Guid Id) : IQuery<Response.PermissionResponse>;
    public record GetAllPermissionsQuery() : IQuery<List<Response.PermissionResponse>>;
}
