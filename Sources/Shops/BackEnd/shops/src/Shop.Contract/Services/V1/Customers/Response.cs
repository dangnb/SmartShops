using static Shop.Contract.Services.V1.PaymentHistories.Response;

namespace Shop.Contract.Services.V1.Customers;
public class Response
{
    public record CustomerResponse(Guid Id, string Code, string Name, string Address, string Email, string PhoneNumber);
    public record CustomerDataEditResponse(Guid Id, string Code, string Name, string Address, string Email, string PhoneNumber, int VillageId, int WardId, int DistrictId, int CityId , IList<PaymentHistoryResponse> Payments);
}
