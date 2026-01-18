namespace Shop.Contract.Services.Purchasing.V1.Warehouses;

public class Response
{
    public record WarehouseResponse(Guid Id, string Code, string Name, string? Address, bool IsActive);
}
