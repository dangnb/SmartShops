
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Wards;
public static class Command
{
    public record CreateWardCommand(int DistrictId, string Code, string Name) : ICommand;
    public record UpdateWardCommand(int Id, int DistrictId, string Code, string Name) : ICommand;
    public record DeleteWardCommand(int Id) : ICommand;
}
