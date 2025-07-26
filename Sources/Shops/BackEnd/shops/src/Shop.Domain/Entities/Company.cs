using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;

public class Company : DomainEntity<int>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Addess { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int NumberAccount { get; set; }

    protected Company() { }

    private Company(string code, string name, string addess, string phone, string mail, int numberAccount)
    {
        Code = code;
        Name = name;
        Addess = addess;
        Phone = phone;
        Mail = mail;
        NumberAccount = numberAccount;
        IsActive = false;
    }
    public static Company CreateEntity(string code, string name, string addess, string phone, string mail, int numberAccount)
    {
        return new Company(code, name, addess, phone, mail, numberAccount);
    }

    public void Update(string name, string addess, string phone, string mail)
    {
        Name = name;
        Addess = addess;
        Phone = phone;
        Mail = mail;
    }

}
