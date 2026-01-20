using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Users;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Users;
public sealed class UpdateUserCommandHandler : ICommandHandler<Command.UpdateUserCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ICurrentUser _userProvider;
    private readonly IPublisher _publisher;


    public UpdateUserCommandHandler(IPublisher publisher,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ICurrentUser userProvider
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _publisher = publisher;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        user.Roles.Clear();
        user.Update(request.UserName, request.FullName, request.Address);
        await _userManager.UpdateAsync(user);
        // Lấy danh sách các vai trò hiện tại của người dùng
        var currentRoles = await _userManager.GetRolesAsync(user);
        var roles = await _roleManager.Roles.Where(x => request.RoleCodes.Contains(x.Name)).ToListAsync();
        // Thêm các vai trò mới cho người dùng
        if (roles.Count > 0)
        {
            foreach (var role in roles)
            {
                var addResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (!addResult.Succeeded)
                {
                    throw new Exception("Failed to add user to roles: " + string.Join(", ", addResult.Errors.Select(e => e.Description)));
                }
            }

        }
        return Result.Success(user);
    }
}
