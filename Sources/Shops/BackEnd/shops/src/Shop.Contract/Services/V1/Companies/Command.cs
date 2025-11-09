
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Companies;
public static class Command
{
    public record CreateCompanyCommand(string Code, string Name, string Addess, string Phone, string Mail, int NumberAccount) : ICommand;
    public record UpdateCompanyCommand(Guid Id, string Code, string Name, string Addess, string Phone, string Mail, int NumberAccount) : ICommand;
    public record LockCompanyCommandHandler(Guid Id) : ICommand;
}
