using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Purchasing.Warehouses;

public class Command
{
    public sealed record CreateWarehouseCommand(string Code, string Name, string? Address, bool IsActive) : ICommand;
    public sealed record UpdateWarehouseCommand(Guid Id, string Name, string? Address, bool IsActive) : ICommand;
    public sealed record DeleteWarehouseCommand(Guid Id) : ICommand;
}
