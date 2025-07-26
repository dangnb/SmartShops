namespace Shop.Contract.Services.V1.Districts;
public class Response
{
    public record DistrictResponse(int Id, int CityId, string Code, string Name, string CityName);
}
