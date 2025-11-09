using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Companies;
public static class DomainEvent
{
    public record CompanyCreated(Guid Id, string TaxCode, string Username) : ICompanyScopedEntity;
}
