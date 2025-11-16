using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Roles;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Queries.Roles;
public class GetRolesQueryHandler : IQueryHandler<Query.GetRolesQuery, List<Response.RoleResponse>>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;

    public GetRolesQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper, ICurrentUser userProvider)
    {
        _roleManager = roleManager;
        _mapper = mapper;
        _userProvider = userProvider;
    }



    public async Task<Result<List<Response.RoleResponse>>> Handle(Query.GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.Where(x => x.ComId == _userProvider.GetRequiredCompanyId()).ToListAsync();
        var result = roles.Select(x => new Response.RoleResponse(x.Id, x.Name, x.Description, x.Permissions.Select(c => c.Code).ToArray())).ToList();
        return Result.Success(result);
    }
}
