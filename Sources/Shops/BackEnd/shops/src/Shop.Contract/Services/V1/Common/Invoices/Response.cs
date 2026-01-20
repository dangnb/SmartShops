namespace Shop.Contract.Services.V1.Common.Invoices;
public class Response
{
    public record InvoiceResponse(int Id, int CityId, string Code, string Name);
}
