
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Contract.Services.V1.Common.Roles;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Roles;
public sealed class DeleteRoleCommandHandler : ICommandHandler<Command.DeleteRoleCommand>
{
    private readonly RoleManager<AppRole> _roleManager;


    public DeleteRoleCommandHandler(
        RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result> Handle(Command.DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x=>x.Id == request.ID);
        if (role == null)
            return Result.Failure(new Error("", "User not found"));
        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            return Result.Success("Delete role success");
        };
        return Result.Failure(new Error("", "Delete user false"));
    }
}
