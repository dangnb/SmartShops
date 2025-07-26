using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Districts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using Shop.Domain.Exceptions;

namespace Shop.Apptication.UserCases.V1.Commands.Districts;
public class DeleteDistrictCommandHandler : ICommandHandler<Command.DeleteDistrictCommand>
{
    private readonly IRepositoryBase<District, int> _repositoryBase;
    public DeleteDistrictCommandHandler(IRepositoryBase<District, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        var district = await _repositoryBase.FindByIdAsync(request.Id) 
            ?? throw new DistrictsException.DistrictNotFoundException(request.Id);
        _repositoryBase.Remove(district);
        return Result.Success();
    }
}
