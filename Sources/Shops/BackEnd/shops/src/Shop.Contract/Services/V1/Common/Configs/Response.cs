namespace Shop.Contract.Services.V1.Common.Configs;
public class Response
{
    public record ConfigResponse(int Id, string Code, string Value);
}
