using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CitiesException;

namespace TP.Apptication.UserCases.V1.Commands.Provincies;

public class DeleteProvincyCommandHandler : ICommandHandler<Command.DeleteProvincyCommand>
{
    private readonly IRepositoryBase<Provincy, Guid> _repositoryBase;
    public DeleteProvincyCommandHandler(IRepositoryBase<Provincy, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteProvincyCommand request, CancellationToken cancellationToken)
    {
        var city = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CityNotFoundException(request.Id);
        _repositoryBase.Remove(city);
        return Result.Success();
    }
}
