using Shop.Contract.Abstractions.Message;
namespace Shop.Contract.Services.V1.Common.Users;
public static class Command
{
    public record CreateUserCommand(string UserName, string FullName, string? FirstName, string? LastName, string? Email, string? Address, string[] RoleCodes) : ICommand;
    public record LoginCommand(string UserName, string PassWord, string TaxCode) : ICommand;
    public record UpdateUserCommand(Guid Id, string UserName, string FullName, string? Address, string[] RoleCodes) : ICommand;
    public record AddDistrictForUserCommand(Guid Id, string[] DistrictCodes) : ICommand;
    public record DeleteUserCommand(Guid Id) : ICommand;
    public record ValidateTokenCommand(string token) : ICommand;
}
