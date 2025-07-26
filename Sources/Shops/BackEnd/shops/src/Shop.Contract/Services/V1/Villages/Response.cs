namespace Shop.Contract.Services.V1.Villages;
public class Response
{
    public record VillageResponse(int Id, int WardId, string Code, string Name, string WardName, string UserName);

    public record VillageDetailResponse(int Id, int WardId, string Code, string Name, string username, string WardName, int DistrictId, int CityId);
}
