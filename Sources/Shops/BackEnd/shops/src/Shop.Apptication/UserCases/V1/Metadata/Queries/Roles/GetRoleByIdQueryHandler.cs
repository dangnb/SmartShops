using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Roles;
using Shop.Domain.Entities.Identity;
using static Shop.Domain.Exceptions.RolesException;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Roles;
public class GetRoleByIdQueryHandler : IQueryHandler<Query.GetRoleByIdQuery, Response.RoleResponse>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;
    public GetRoleByIdQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper, ICurrentUser userProvider)
    {
        _roleManager = roleManager;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.RoleResponse>> Handle(Query.GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id.ToString()) ?? throw new RoleNotFoundException(request.Id);
        return Result.Success(new Response.RoleResponse(role.Id, role.Name, role.Description, role.Permissions.Select(x=>x.Code).ToArray()));
    }
}
