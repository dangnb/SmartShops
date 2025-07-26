
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Configs;
public static class Command
{
    public record CreateConfigCommand(string Code, string Value) : ICommand;
    public record UpdateConfigCommand(int Id, string Code, string Value) : ICommand;
    public record DeleteConfigCommand(int Id) : ICommand;
}
