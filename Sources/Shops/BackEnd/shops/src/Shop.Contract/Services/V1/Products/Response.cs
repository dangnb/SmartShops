namespace Shop.Contract.Services.V1.Products;
public class Response
{
    public record ProductResponse(int Id, string Code, string Name, decimal Price, bool IsActive, int ProductType);
}

