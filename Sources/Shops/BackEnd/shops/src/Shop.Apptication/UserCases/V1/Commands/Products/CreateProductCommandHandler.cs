using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Products;
public class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
{
    private readonly IRepositoryBase<Product, int> _repositoryBase;
    private readonly IUserProvider _userProvider;
    public CreateProductCommandHandler(IRepositoryBase<Product, int> repositoryBase, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
    }
    public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = Product.CreateEntity(_userProvider.GetComID(), request.Code, request.Name, request.Price, request.IsActive, request.ProductType);
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
