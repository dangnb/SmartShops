
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Invoices;
public static class Command
{
    public record CreateInvoiceCommand(int CityId, string Code, string Name) : ICommand;
    public record UpdateInvoiceCommand(int Id, int CityId, string Code, string Name) : ICommand;
    public record DeleteInvoiceCommand(int Id) : ICommand;
}
