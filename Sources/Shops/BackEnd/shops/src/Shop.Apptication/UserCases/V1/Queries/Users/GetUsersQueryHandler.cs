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
public class GetUsersQueryHandler : IQueryHandler<Query.GetUsersQuery, PagedResult<UserResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserProvider _userProvider;

    public GetUsersQueryHandler(IMapper mapper, IUserRepository userRepository, IUserProvider userProvider)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userProvider = userProvider;
    }
    public async Task<Result<PagedResult<UserResponse>>> Handle(Query.GetUsersQuery request, CancellationToken cancellationToken)
    {
        var comId = _userProvider.GetComID();
        StringBuilder stringBuilder = new StringBuilder("Select us.Id, us.UserName, ");
        stringBuilder.Append("us.FullName,us.TaxCode,us.LastName, ");
        stringBuilder.Append("us.DayOfBirth,us.Email,us.PhoneNumber ");
        stringBuilder.Append("FROM appusers AS us ");
        stringBuilder.Append("where us.ComId = @comid ");
        List<Domain.Common.SQLParam> sQLParams =
        [
            new Domain.Common.SQLParam("comid", comId.ToString())
        ];
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            stringBuilder.Append(" and (us.FullName like @searchTerm or us.UserName like @searchTerm)");
            sQLParams.Add(new Domain.Common.SQLParam("searchTerm", $"{request.SearchTerm}%"));
        }
        var (users, total) = await _userRepository.GetDynamicPagedAsync<AppUser>(stringBuilder.ToString(), request.PageIndex, request.PageSize, sQLParams.ToArray());
        var page = PagedResult<AppUser>.Create(users.ToList(), request.PageIndex, request.PageSize, total);
        var result = _mapper.Map<PagedResult<UserResponse>>(page);
        return Result.Success(result);
    }
}
