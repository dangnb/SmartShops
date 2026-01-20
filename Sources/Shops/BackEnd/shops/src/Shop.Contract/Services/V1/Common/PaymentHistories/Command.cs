using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Common.PaymentHistories;
public static class Command
{
    public record CreateCustomerHistoryCommand(int type, int quantity, decimal price) : ICommand;
    public record UpdateCustomerHistoryCommand(int id, int type, int quantity, decimal price) : ICommand;

}
