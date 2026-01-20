namespace Shop.Contract.Services.V1.Common.PaymentHistories;
public class Response
{
    public record PaymentHistoryResponse(int Id, int Quantity, decimal Price, int Type);
}
