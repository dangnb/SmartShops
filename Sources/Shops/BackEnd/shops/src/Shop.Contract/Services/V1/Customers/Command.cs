
using Microsoft.AspNetCore.Http;
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Customers;
public static class Command
{
    public record CreateCustomerCommand(string Code, string Name, string Address, string Email, string PhoneNumber, string CitizenIdNumber, string PassportNumber) : ICommand;
    public record UpdateCustomerCommand(Guid Id, string Code, string Name, string Address, string Email, string PhoneNumber, string CitizenIdNumber, string PassportNumber) : ICommand;
    public record DeleteCustomerCommand(Guid Id) : ICommand;
    public record UploadCustomerCommand(IFormFile File) : ICommand;
}
