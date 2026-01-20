
using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Users;
using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Users;
public sealed class DeleteRoleCommandHandler : ICommandHandler<Command.DeleteUserCommand>
{
    //private readonly IRepositoryBase<Domain.Entities.Identity.AppUser, Guid> _userRepositoryBase;
    private readonly UserManager<AppUser> _userManager;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteRoleCommandHandler(
        //IRepositoryBase<Domain.Entities.Identity.AppUser, Guid> userRepositoryBase,
        UserManager<AppUser> userManager,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        //_userRepositoryBase = userRepositoryBase;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            return Result.Failure(new Error("", "User not found"));
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return Result.Success("Delete user success");
        };
        return Result.Failure(new Error("", "Delete user false"));
    }
}
