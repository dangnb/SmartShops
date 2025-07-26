using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Entities.Identity;

namespace Shop.Application.UserCases.V1.Commands.Users;
public sealed class CreateUserCommandHandler : ICommandHandler<Command.CreateUserCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IUserProvider _userProvider;
    private readonly IPublisher _publisher;


    public CreateUserCommandHandler(IPublisher publisher,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IUserProvider userProvider
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _publisher = publisher;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passDefault = "Abcd@123456a";
        var comId = _userProvider.GetComID();
        var taxCode = _userProvider.GetTaxCode();
        var userExists = await _userManager.FindByNameAsync(request.UserName);
        if (userExists != null)
        {
            return Result.Failure(new Error("EXISTS", "Username exists!"));
        }

        var roles = await _roleManager.Roles.Where(x => request.RoleCodes.Contains(x.Name)).ToListAsync();

        var user = AppUser.CreateEntity(comId, request.UserName, request.FullName,
          passDefault, request.FirstName, request.LastName, request.Email, taxCode, request.Address);

        var result = await _userManager.CreateAsync(user, passDefault);
        if (!result.Succeeded)
        {
            return Result.Failure(new Error("CREATE_FAlSE", "Account creation failed. Please try again later!"));
        };
        foreach (var role in roles)
        {
            await _userManager.AddToRoleAsync(user, role.Name);

        }
        return Result.Success(user);
    }
}
