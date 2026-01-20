using System.Text;
using AutoMapper;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Users;
using Shop.Domain.Dappers.Repositories;
using Shop.Domain.Entities.Identity;
using static Shop.Contract.Services.V1.Common.Users.Response;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Users;
public class GetAllUserQueryHandler : IQueryHandler<Query.GetAllUserQuery, IList<UserResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _userProvider;

    public GetAllUserQueryHandler(IMapper mapper, IUserRepository userRepository, ICurrentUser userProvider)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<UserResponse>>> Handle(Query.GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var comId = _userProvider.GetRequiredCompanyId();
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
