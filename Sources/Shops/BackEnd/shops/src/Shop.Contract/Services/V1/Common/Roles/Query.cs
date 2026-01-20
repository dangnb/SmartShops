using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Enumerations;

namespace Shop.Contract.Services.V1.Common.Roles;
public static class Query
{
    public record GetRolesQuery() : IQuery<List<Response.RoleResponse>>;
    public record GetRoleByIdQuery(Guid Id) : IQuery<Response.RoleResponse>;
}
