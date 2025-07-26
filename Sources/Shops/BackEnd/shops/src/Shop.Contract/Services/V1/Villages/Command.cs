
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Villages;
public static class Command
{
    public record CreateVillageCommand(int WardId, string Code, string Name, string username) : ICommand;
    public record UpdateVillageCommand(int Id, int WardId, string Code, string Name, string username) : ICommand;
    public record DeleteVillageCommand(int Id) : ICommand;
    public record AddUserNameToVillageCommand(int Id, string username) : ICommand;
}
