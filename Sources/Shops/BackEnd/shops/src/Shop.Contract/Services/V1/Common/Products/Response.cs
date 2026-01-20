namespace Shop.Contract.Services.V1.Common.Products;

public class Response
{
    public record ProductResponse(Guid Id, string Code, string Name, Guid CategoryId, int Status);
}

