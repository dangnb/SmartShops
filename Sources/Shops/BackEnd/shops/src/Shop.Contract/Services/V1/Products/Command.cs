
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Products;
public static class Command
{
    public record CreateProductCommand(string Code, string Name, decimal Price, bool IsActive, int ProductType) : ICommand;
    public record UpdateProductCommand(int Id, string Code, string Name, decimal Price, bool IsActive, int ProductType) : ICommand;
    public record DeleteProductCommand(int Id) : ICommand;
}
