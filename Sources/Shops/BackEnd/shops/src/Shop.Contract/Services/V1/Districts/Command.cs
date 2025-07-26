
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Districts;
public static class Command
{
    public record CreateDistrictCommand(int CityId, string Code, string Name) : ICommand;
    public record UpdateDistrictCommand(int Id, int CityId, string Code, string Name) : ICommand;
    public record DeleteDistrictCommand(int Id) : ICommand;
}
