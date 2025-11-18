namespace Shop.Contract.Services.V1.Wards;
public class Response
{
    public record WardResponse(Guid Id, Guid ProvinceId, string Code, string Name, string ProvinceName);
    public record WardDetailResponse(Guid Id, Guid ProvinceId, string Code, string Name, string ProvinceName);
}
