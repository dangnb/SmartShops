using Shop.Contract.Abstractions.Message;
namespace Shop.Contract.Services.V1.Common.Payments;
public static class Command
{
    public record CreatePaymentCommand(Guid CustomerId, int Year, IList<PaymentOfMonthCommand> PaymentOfMonths) : ICommand;

    public record PaymentOfMonthCommand(int Type, decimal Price, int Quantity, string Month, string Note);
}
