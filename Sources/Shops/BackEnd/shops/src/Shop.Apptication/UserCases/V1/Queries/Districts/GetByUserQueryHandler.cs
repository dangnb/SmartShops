using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Districts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Dappers.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Districts;
public class GetByUserQueryHandler : IQueryHandler<Query.GetByUserQuery, IList<Response.DistrictResponse>>
{
    private readonly IDistrictRepository _districtRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetByUserQueryHandler(IDistrictRepository districtRepository, IMapper mapper, IUserProvider userProvider)
    {
        _districtRepository = districtRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<Response.DistrictResponse>>> Handle(Query.GetByUserQuery request, CancellationToken cancellationToken)
    {
        var userName = _userProvider.GetUserName();
        var comId = _userProvider.GetComID();
        StringBuilder stringBuilder = new StringBuilder("SELECT d.*  FROM  appusers as us ");
        stringBuilder.Append("JOIN appuserdistricts as ud ON us.Id = ud.UserId  ");
        stringBuilder.Append("JOIN districts AS d ON ud.DistrictId = d.Id ");
        stringBuilder.Append("WHERE us.UserName = @UserName and us.ComId = @comid");

        List<Domain.Common.SQLParam> sQLParams =
               [
                    new Domain.Common.SQLParam("UserName", userName),
                   new Domain.Common.SQLParam("comid", comId.ToString())
               ];
        var districts = await _districtRepository.GetQueryAsync<District>(stringBuilder.ToString(), sQLParams.ToArray());
        var result = _mapper.Map<IList<Response.DistrictResponse>>(districts);
        return Result.Success(result);
    }
}
