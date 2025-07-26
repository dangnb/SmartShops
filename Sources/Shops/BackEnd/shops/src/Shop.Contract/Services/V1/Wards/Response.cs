namespace Shop.Contract.Services.V1.Wards;
public class Response
{
    public record WardResponse(int Id, int DistrictId, string Code, string Name, string DistrictName);
    public record WardDetailResponse(int Id, int DistrictId, string Code, string Name, string DistrictName, int CityId);
}
