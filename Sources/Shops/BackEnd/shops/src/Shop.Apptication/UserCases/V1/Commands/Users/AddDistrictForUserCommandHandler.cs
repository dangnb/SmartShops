using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;
using Shop.Domain.Entities.Metadata;

namespace Shop.Application.UserCases.V1.Commands.Users;
public sealed class AddDistrictForUserCommandHandler : ICommandHandler<Command.AddDistrictForUserCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRepositoryBase<District, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    private readonly IPublisher _publisher;


    public AddDistrictForUserCommandHandler(IPublisher publisher,
        UserManager<AppUser> userManager,
        IRepositoryBase<District, int> repositoryBase,
        IUserProvider userProvider
        )
    {
        _userManager = userManager;
        _repositoryBase = repositoryBase;
        _publisher = publisher;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.AddDistrictForUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        var districts = await _repositoryBase.FindAll().Where(x => request.DistrictCodes.Contains(x.Code)).ToListAsync();
        user.Districts.Clear();
        // Lấy danh sách các vai trò hiện tại của người dùng
        // Thêm các vai trò mới cho người dùng
        if (districts.Count > 0)
        {
            foreach (var district in districts)
            {
                user.Districts.Add(new AppUserDistrict() { DistrictId = district.Id, UserId = user.Id });
            }

        }
        await _userManager.UpdateAsync(user);
        return Result.Success(user);
    }
}
