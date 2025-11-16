using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Roles;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;

namespace Shop.Application.UserCases.V1.Commands.Roles;
public sealed class CreateRoleCommandHandler : ICommandHandler<Command.CreateRoleCommand>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IPublisher _publisher;
    private readonly ICurrentUser _userProvider;
    private readonly IRepositoryBase<Permission, Guid> _permissionRepository;


    public CreateRoleCommandHandler(
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
    public async Task<Result> Handle(Command.CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var userame = _userProvider.UserId;
        var comId = _userProvider.GetRequiredCompanyId();
        var userExists = await _roleManager.Roles.AnyAsync(x => x.Name == request.Name);
        if (userExists)
        {
            return Result.Failure(new Error("EXISTS", "RoleCode exists!"));
        }
        var permissions = await _permissionRepository.FindAll().Where(x => request.PermissionCodes.Contains(x.Code)).ToListAsync();
        var role = AppRole.CreateEntity(comId, request.Description, request.Name);
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            return Result.Failure(new Error("CREATE_ROLE", "Role creation failed. Please try again later!"));
        };
        foreach (var item in permissions)
        {
            await _roleManager.AddClaimAsync(role, new Claim(item.Code, item.Description));
        }
        return Result.Success(role);
    }
}
