using static Shop.Contract.Services.V1.PaymentHistories.Response;

namespace Shop.Contract.Services.V1.Customers;
public class Response
{
    public record CustomerResponse(Guid Id, string Code, string Name, string Address, string Email, string PhoneNumber, string CitizenIdNumber, string PassportNumber);
}
