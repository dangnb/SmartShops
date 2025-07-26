
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Cities;
public static class Command
{
    public record CreateCityCommand(string Code, string Name) : ICommand;
    public record UpdateCityCommand(int Id, string Code, string Name) : ICommand;
    public record DeleteCityCommand(int Id) : ICommand;
}
