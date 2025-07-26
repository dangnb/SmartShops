﻿using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Commands.Wards;
public class DeleteWardCommandHandler : ICommandHandler<Command.DeleteWardCommand>
{
    private readonly IRepositoryBase<Ward, int> _repositoryBase;
    public DeleteWardCommandHandler(IRepositoryBase<Ward, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteWardCommand request, CancellationToken cancellationToken)
    {
        var ward = await _repositoryBase.FindByIdAsync(request.Id)
            ?? throw new WardNotFoundException(request.Id);
        _repositoryBase.Remove(ward);
        return Result.Success();
    }
}
