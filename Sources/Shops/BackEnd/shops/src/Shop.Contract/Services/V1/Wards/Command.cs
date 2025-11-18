
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Wards;
public static class Command
{
    public record CreateWardCommand(Guid ProvinceId, string Code, string Name) : ICommand;
    public record UpdateWardCommand(Guid Id, Guid ProvinceId, string Code, string Name) : ICommand;
    public record DeleteWardCommand(Guid Id) : ICommand;
}
