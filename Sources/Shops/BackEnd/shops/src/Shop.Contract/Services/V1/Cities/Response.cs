namespace Shop.Contract.Services.V1.Cities;
public class Response
{
    public record CityResponse(int Id, string Code, string Name);
}
