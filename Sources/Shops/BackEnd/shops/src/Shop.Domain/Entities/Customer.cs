using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;

public class Customer : DomainEntity<Guid>, ICompanyScopedEntity
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string CitizenIdNumber { get; private set; } = string.Empty;
    public string PassportNumber { get; private set; } = string.Empty;
    public bool IsLock { get; private set; }
    public Guid ComId { get; private set; }

    protected Customer() { }

    private Customer(Guid comId, string code, string name, string address, string email, string phoneNumber, string citizenIdNumber, string passportNumber)
    {
        Code = code;
        Name = name;
        ComId = comId;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        CitizenIdNumber = citizenIdNumber;
        PassportNumber = passportNumber;
    }
    public static Customer CreateEntity(Guid comId, string code, string name, string address, string email, string phoneNumber, string citizenIdNumber, string passportNumber)
    {
        return new Customer(comId,code, name, address, email, phoneNumber, citizenIdNumber, passportNumber);
    }

    public void Update(string name, string address, string email, string phoneNumber, string citizenIdNumber, string passportNumber)
    {
        Name = name;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        CitizenIdNumber = citizenIdNumber;
        PassportNumber = passportNumber;
    }
}
