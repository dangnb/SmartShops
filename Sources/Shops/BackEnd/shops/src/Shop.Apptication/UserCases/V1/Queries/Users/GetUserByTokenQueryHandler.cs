using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;

namespace Shop.Apptication.UserCases.V1.Queries.Users;
public class GetUserByTokenQueryHandler : IQueryHandler<Query.GetUserByTokenQuery, Response.UserInforByToken>
{

    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserProvider _userProvider;
    public GetUserByTokenQueryHandler(IMapper mapper, UserManager<AppUser> userManager, IUserProvider userProvider)
    {
        _mapper = mapper;
        _userManager = userManager;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.UserInforByToken>> Handle(Query.GetUserByTokenQuery request, CancellationToken cancellationToken)
    {
        var username = _userProvider.GetUserName();
        var taxcode = _userProvider.GetTaxCode();
        var user = await _userManager.Users.Where(x => x.TaxCode == taxcode && x.UserName == username).FirstOrDefaultAsync();
        var result = _mapper.Map<Response.UserInforByToken>(user);
        return Result.Success(result);
    }
}
