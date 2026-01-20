using System.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Roles;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Roles;
public sealed class UpdateRoleCommandHandler : ICommandHandler<Command.UpdateRoleCommand>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IPublisher _publisher;
    private readonly ICurrentUser _userProvider;
    private readonly IRepositoryBase<Permission, Guid> _permissionRepository;


    public UpdateRoleCommandHandler(
        IPublisher publisher,
        RoleManager<AppRole> roleManager,
        IRepositoryBase<Permission, Guid> permissionRepository,
        ICurrentUser userProvider
        )
    {
        _userProvider = userProvider;
        _permissionRepository = permissionRepository;
        _roleManager = roleManager;
        _publisher = publisher;
    }
    public async Task<Result> Handle(Command.UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var userame = _userProvider.UserId;
        var comId = _userProvider.GetRequiredCompanyId();
        var role = await _roleManager.Roles.FirstAsync(x => x.Id == request.ID);
        if (role == null)
        {
            return Result.Failure(new Error("NOT_FOUND", "Role not exists!"));
        }
        var permissions = await _permissionRepository.FindAll().Where(x => request.PermissionCodes.Contains(x.Code)).ToListAsync();

        role.UpdateEntity(request.Description, request.Name);
        role.Permissions = role !=null  && role.Permissions != null
            ? role.Permissions.Where(x => request.PermissionCodes.Contains(x.Code)).ToList()
            : (ICollection<Permission>)new List<Permission>();

        foreach (var item in permissions)
        {
            if (!role.Permissions.Any(x => x.Code == item.Code))
            {
                role.Permissions.Add(item);
            }
        }
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            return Result.Failure(new Error("UPDATE_ROLE", "Role updation failed. Please try again later!"));
        };
        return Result.Success(role);
    }
}
