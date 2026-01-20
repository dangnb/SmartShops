namespace Shop.Contract.Services.V1.Purchasing.Warehouses;

public class Response
{
    public record WarehouseResponse(Guid Id, string Code, string Name, string? Address, bool IsActive);
}
