namespace Shop.Contract.Services.V1.Common.Provincies;
public class Response
{
    public record ProvinceResponse(Guid Id, string Code, string Name);
}
