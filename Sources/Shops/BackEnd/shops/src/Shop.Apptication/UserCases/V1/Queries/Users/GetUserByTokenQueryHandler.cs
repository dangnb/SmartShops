using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Queries.Users;
public class GetUserByTokenQueryHandler : IQueryHandler<Query.GetUserByTokenQuery, Response.UserInforByToken>
{

    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICurrentUser _userProvider;
    public GetUserByTokenQueryHandler(IMapper mapper, UserManager<AppUser> userManager, ICurrentUser userProvider)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.UserInforByToken>> Handle(Query.GetUserByTokenQuery request, CancellationToken cancellationToken)
    {
        var username = _userProvider.UserId;
        var comId = _userProvider.GetRequiredCompanyId();
        var user = await _userManager.Users.Where(x => x.ComId == comId && x.UserName == username).FirstOrDefaultAsync();
        var result = _mapper.Map<Response.UserInforByToken>(user);
        return Result.Success(result);
    }
}
