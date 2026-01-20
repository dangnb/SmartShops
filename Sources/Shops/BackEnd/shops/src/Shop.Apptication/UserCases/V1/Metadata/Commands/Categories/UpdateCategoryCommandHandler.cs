using MediatR;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Categories;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CategoriesException;
using static Shop.Domain.Exceptions.CustomersException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Categories;

public class UpdateCategoryCommandHandler : ICommandHandler<Command.UpdateCategoryCommand>
{
    private readonly IRepositoryBase<Category, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;
    public UpdateCategoryCommandHandler(IRepositoryBase<Category, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        if (request.ParentId.HasValue)
        {
            Category parentCategory = await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Id == request.ParentId.Value)
            ?? throw new CategoryNotFoundParentException();
        }
        var entity = await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Id == request.Id) ?? throw new CustomerNotFoundException(request.Id);
        entity.Update(request.Name, request.ParentId, request.SortOrder, request.Level, request.IsActive);
        _repositoryBase.Update(entity);
        return Result.Success(entity);
    }
}
