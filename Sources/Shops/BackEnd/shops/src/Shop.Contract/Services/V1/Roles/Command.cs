
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Roles;
public static class Command
{
    public record CreateRoleCommand(string Name, string Description, string[] PermissionCodes) : ICommand;
    public record UpdateRoleCommand(Guid ID, string Name, string Description, string[] PermissionCodes) : ICommand;
    public record DeleteRoleCommand(Guid ID) : ICommand;
}
