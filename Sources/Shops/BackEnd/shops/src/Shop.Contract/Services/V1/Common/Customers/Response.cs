using static Shop.Contract.Services.V1.Common.PaymentHistories.Response;

namespace Shop.Contract.Services.V1.Common.Customers;
public class Response
{
    public record CustomerResponse(Guid Id, string Code, string Name, string Address, string Email, string PhoneNumber, string CitizenIdNumber, string PassportNumber);
}
