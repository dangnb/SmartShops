using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.Purchasing.V1.Warehouses;

public class Command
{
    public sealed record CreateWarehouseCommand(string Code, string Name, string? Address, bool IsActive) : ICommand;
    public sealed record UpdateWarehouseCommand(Guid Id, string Name, string? Address, bool IsActive) : ICommand;
    public sealed record DeleteWarehouseCommand(Guid Id) : ICommand;
}
