
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Products;

public static class Command
{
    public record CreateProductCommand(string Code, string Name, string BarCode, Guid CategoryId, bool IsActive) : ICommand;
    public record UpdateProductCommand(Guid Id,  string Name, string BarCode, Guid CategoryId) : ICommand;
    public record DeleteProductCommand(Guid Id) : ICommand;
}
