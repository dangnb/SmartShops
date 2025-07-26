using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Customer : DomainEntity<Guid>
{
    public string Code { get; private set; } = string.Empty;
    public int ComId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public int VillageId { get; private set; }
    public bool IsLock { get; private set; }
    public virtual ICollection<PaymentHistory> PaymentHistories { get; set; }

    protected Customer() { }

    private Customer(int comId, string code, string name, string address, string email, string phoneNumber, int villageId)
    {
        Code = code;
        ComId = comId;
        Name = name;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        VillageId = villageId;
    }
    public static Customer CreateEntity(int comId, string code, string name, string address, string email, string phoneNumber, int villageId)
    {
        return new Customer(comId, code, name, address, email, phoneNumber, villageId);
    }

    public void Update(string code, string name, string address, string email, string phoneNumber, int villageId)
    {
        Code = code;
        Name = name;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        VillageId = villageId;
    }
}
