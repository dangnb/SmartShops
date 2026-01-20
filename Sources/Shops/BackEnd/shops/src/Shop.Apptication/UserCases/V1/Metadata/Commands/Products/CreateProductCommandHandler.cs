using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Products;

public class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
{
    private readonly IRepositoryBase<Product, Guid> _repositoryBase;

    public CreateProductCommandHandler(
        IRepositoryBase<Product, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Check trùng Code (nếu cần)
        var exists = await _repositoryBase
            .AnyAsync(x => x.Code == request.Code, cancellationToken);

        if (exists)
        {
            // Bạn có thể throw custom DomainException/ValidationException tùy framework
            throw new InvalidOperationException($"Sản phẩm có mã '{request.Code}' đã tồn tại trên hệ thống.");
        }


        var product = Product.CreateEntity(
            request.Code,
            request.Name,
            request.BarCode,
            request.CategoryId
        );

        _repositoryBase.Add(product);
        return Result.Success();
    }
}
