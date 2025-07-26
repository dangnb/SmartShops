using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Contract.Abstractions.Message;
using static Shop.Contract.Services.V1.Users.Response;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Apptication.Exceptions;
using Shop.Domain.Entities.Identity;
using Shop.Domain.Dappers.Repositories;
using Shop.Contract.Services.V1.Payments.Dtos;

namespace Shop.Apptication.UserCases.V1.Queries.Users;
public class GetAllUserQueryHandler : IQueryHandler<Query.GetAllUserQuery, IList<UserResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserProvider _userProvider;

    public GetAllUserQueryHandler(IMapper mapper, IUserRepository userRepository, IUserProvider userProvider)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<UserResponse>>> Handle(Query.GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var comId = _userProvider.GetComID();
        StringBuilder stringBuilder = new StringBuilder("Select * ");
        stringBuilder.Append("FROM appusers AS us ");
        stringBuilder.Append("where us.ComId = @comid ");
        List<Domain.Common.SQLParam> sQLParams =
        [
            new Domain.Common.SQLParam("comid", comId.ToString())
        ];
        var users = await _userRepository.GetQueryAsync<AppUser>(stringBuilder.ToString(), sQLParams.ToArray());
        var result = _mapper.Map<IList<UserResponse>>(users);
        return Result.Success(result);
    }
}
