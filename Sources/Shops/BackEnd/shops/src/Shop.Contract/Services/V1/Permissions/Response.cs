namespace Shop.Contract.Services.V1.Permissions;
public class Response
{
    public record PermissionResponse(Guid Id, string Description, string Code, string GroupCode, string GroupName);
}
