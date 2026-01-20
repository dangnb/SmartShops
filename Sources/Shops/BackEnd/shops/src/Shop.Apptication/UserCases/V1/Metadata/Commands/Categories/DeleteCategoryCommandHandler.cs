using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Categories;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CustomersException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Categories;

public class DeleteCatigoryCommandHandler : ICommandHandler<Command.DeleteCategoryCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;
    public DeleteCatigoryCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        var entity = await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Id == request.Id) ?? throw new CustomerNotFoundException(request.Id);
        _repositoryBase.Remove(entity);
        return Result.Success(entity);
    }
}
