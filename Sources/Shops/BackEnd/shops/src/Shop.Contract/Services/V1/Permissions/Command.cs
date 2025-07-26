
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Permissions;
public static class Command
{
    public record CreatePermissionCommand(string Description, string Code, string GroupCode, string GroupName) : ICommand;
    public record UpdatePermissionCommand(Guid ID, string Description, string Code, string GroupCode, string GroupName) : ICommand;
    public record DeletePermissionCommand(Guid ID) : ICommand;
}
