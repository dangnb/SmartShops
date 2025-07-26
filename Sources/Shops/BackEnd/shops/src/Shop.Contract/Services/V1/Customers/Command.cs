
using Microsoft.AspNetCore.Http;
using Shop.Contract.Abstractions.Message;
using static Shop.Contract.Services.V1.PaymentHistories.Command;

namespace Shop.Contract.Services.V1.Customers;
public static class Command
{
    public record CreateCustomerCommand(string Code, string Name, string Address, string Email, string PhoneNumber, int VillageId, List<CreateCustomerHistoryCommand> payments) : ICommand;
    public record UpdateCustomerCommand(Guid Id, string Code, string Name, string Address, string Email, string PhoneNumber, int VillageId, List<UpdateCustomerHistoryCommand> payments) : ICommand;
    public record DeleteCustomerCommand(Guid Id) : ICommand;
    public record UploadCustomerCommand(IFormFile File, int VillageId) : ICommand;
}
