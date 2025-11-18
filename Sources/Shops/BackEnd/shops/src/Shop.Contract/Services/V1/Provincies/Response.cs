namespace Shop.Contract.Services.V1.Provinces;
public class Response
{
    public record ProvinceResponse(Guid Id, string Code, string Name);
}
