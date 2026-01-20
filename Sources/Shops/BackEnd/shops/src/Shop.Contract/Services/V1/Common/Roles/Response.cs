namespace Shop.Contract.Services.V1.Common.Roles;
public class Response
{
    public record RoleResponse(Guid Id, string Name, string Description, string[] PermissionCodes);
}
