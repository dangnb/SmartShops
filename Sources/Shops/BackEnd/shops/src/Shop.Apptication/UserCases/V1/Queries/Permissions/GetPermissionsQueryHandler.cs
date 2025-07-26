using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Permissions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Queries.Permissions;
public class GetPermissionsQueryHandler : IQueryHandler<Query.GetPermissionsQuery, PagedResult<Response.PermissionResponse>>
{
    private readonly IRepositoryBase<Permission, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;

    public GetPermissionsQueryHandler(IRepositoryBase<Permission, Guid> repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.PermissionResponse>>> Handle(Query.GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissionQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll()
          : _repositoryBase.FindAll(x => x.Description.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm));
        var products = await PagedResult<Permission>.CreateAsync(permissionQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.PermissionResponse>>(products);
        return Result.Success(result);
    }
}
