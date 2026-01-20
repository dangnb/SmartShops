using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Permissions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Permissions;
public class GetAllPermissionsQueryHandler : IQueryHandler<Query.GetAllPermissionsQuery, List<Response.PermissionResponse>>
{
    private readonly IRepositoryBase<Permission, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;

    public GetAllPermissionsQueryHandler(IRepositoryBase<Permission, Guid> repositoryBase, IMapper mapper, ICurrentUser userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<List<Response.PermissionResponse>>> Handle(Query.GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _repositoryBase.FindAll().ToListAsync();
        var result = _mapper.Map<List<Response.PermissionResponse>>(permissions);
        return Result.Success(result);
    }
}
