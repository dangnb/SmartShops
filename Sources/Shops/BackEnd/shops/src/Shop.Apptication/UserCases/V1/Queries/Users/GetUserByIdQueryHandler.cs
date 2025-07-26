using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Dappers.Repositories;
using Shop.Domain.Entities.Identity;
using static Shop.Contract.Services.V1.Users.Response;

namespace Shop.Apptication.UserCases.V1.Queries.Users;
public class GetUserByIdQueryHandler : IQueryHandler<Query.GetUserByIdQuery, UserDetailResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IUserProvider _userProvider;
    private readonly IPublisher _publisher;


    public GetUserByIdQueryHandler(IPublisher publisher,
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
    public async Task<Result<UserDetailResponse>> Handle(Query.GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        var roleNames = await _userManager.GetRolesAsync(user);
        var roles = await _roleManager.Roles.Where(x => roleNames.Contains(x.Name)).ToListAsync();
        return Result.Success(new UserDetailResponse(user.Id,user.UserName, user.FullName, user.TaxCode
            , user.LastName, user.DayOfBirth, user.Email, user.PhoneNumber, roles.Select(x=>x.Name).ToArray()));
    }
}
