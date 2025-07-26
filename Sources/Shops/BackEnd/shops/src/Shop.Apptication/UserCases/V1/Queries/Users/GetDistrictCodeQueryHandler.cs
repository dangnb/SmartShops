using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Dappers.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Queries.Users;
public class GetDistrictCodeQueryHandler : IQueryHandler<Query.GetDistrictCodeQuery, string[]>
{

    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IUserProvider _userProvider;
    public GetDistrictCodeQueryHandler(IMapper mapper, UserManager<AppUser> userManager, IUserRepository userRepository, IUserProvider userProvider)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userProvider = userProvider;
        _userRepository = userRepository;
    }
    public async Task<Result<string[]>> Handle(Query.GetDistrictCodeQuery request, CancellationToken cancellationToken)
    {
        var comId = _userProvider.GetComID();
        StringBuilder stringBuilder = new StringBuilder("SELECT d.Code  FROM  appusers as us ");
        stringBuilder.Append("JOIN appuserdistricts as ud ON us.Id = ud.UserId  ");
        stringBuilder.Append("JOIN districts AS d ON ud.DistrictId = d.Id ");
        stringBuilder.Append("WHERE us.id = @id and us.ComId = @comid");

        List<Domain.Common.SQLParam> sQLParams =
               [
                    new Domain.Common.SQLParam("id", request.Id.ToString()),
                   new Domain.Common.SQLParam("comid", comId.ToString())
               ];
        var data = await _userRepository.GetQueryAsync<string>(stringBuilder.ToString(), sQLParams.ToArray());
        return Result.Success(data.ToArray());
    }
}
