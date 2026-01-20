using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Common.Provincies;
public static class Command
{
    public record CreateProvinceCommand(string Code, string Name) : ICommand;
    public record UpdateProvinceCommand(Guid Id, string Code, string Name) : ICommand;
    public record DeleteProvinceCommand(Guid Id) : ICommand;
}
