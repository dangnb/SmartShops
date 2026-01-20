using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Wards;
public class DeleteWardCommandHandler : ICommandHandler<Command.DeleteWardCommand>
{
    private readonly IRepositoryBase<Ward, Guid> _repositoryBase;
    public DeleteWardCommandHandler(IRepositoryBase<Ward, Guid> repositoryBase)
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
