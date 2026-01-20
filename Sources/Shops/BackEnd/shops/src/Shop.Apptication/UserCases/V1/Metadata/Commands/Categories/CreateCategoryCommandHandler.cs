using MediatR;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Categories;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CategoriesException;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Categories;

public class CreateCategoryCommandHandler : ICommandHandler<Command.CreateCategoryCommand>
{
    private readonly IRepositoryBase<Category, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;
    public CreateCategoryCommandHandler(IRepositoryBase<Category, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        if (request.ParentId.HasValue)
        {
            Category parentCategory = await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Id == request.ParentId.Value)
            ?? throw new CategoryNotFoundParentException();
        }
        var entity = Category.CreateEntity(request.Name, request.Code, request.ParentId, request.SortOrder, request.Level, request.IsActive);
        if (await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Code == entity.Code) != null)
        {
            throw new CategoryDublicateCodeException();
        }
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
