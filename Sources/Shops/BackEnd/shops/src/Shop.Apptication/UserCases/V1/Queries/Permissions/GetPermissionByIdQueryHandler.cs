using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Permissions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.PermissionsException;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Queries.Permissions;
public class GetPermissionByIdQueryHandler : IQueryHandler<Query.GetPermissionByIdQuery, Response.PermissionResponse>
{
    private readonly IRepositoryBase<Permission, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetPermissionByIdQueryHandler(IRepositoryBase<Permission, Guid> repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.PermissionResponse>> Handle(Query.GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new PermissionNotFoundException(request.Id);
        var result = _mapper.Map<Response.PermissionResponse>(city);
        return Result.Success(result);
    }
}
