namespace Shop.Contract.Services.V1.Wards;
public class Response
{
    public record WardResponse(Guid Id, int ProvincyId, string Code, string Name, string DistrictName);
    public record WardDetailResponse(Guid Id, Guid ProvincyId, string Code, string Name, string DistrictName);
}
