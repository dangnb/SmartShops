
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Provincies;
public static class Command
{
    public record CreateProvincyCommand(string Code, string Name) : ICommand;
    public record UpdateProvincyCommand(int Id, string Code, string Name) : ICommand;
    public record DeleteProvincyCommand(int Id) : ICommand;
}
