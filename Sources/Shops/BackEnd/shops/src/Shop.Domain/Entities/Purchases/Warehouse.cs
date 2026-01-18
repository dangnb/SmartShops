using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities.Purchases;

public class Warehouse : EntityAuditBase<Guid>
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Address { get; private set; }
    public bool IsActive { get; private set; } = true;
    protected Warehouse() { }

    public Warehouse(string code, string name)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Warehouse code is required.", nameof(code));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Warehouse name is required.", nameof(name));
        Code = code.Trim();
        Name = name.Trim();
    }
    public static Warehouse CreateEntity(string name, string code,  string? address, bool isActive)
    {
        var warehouse = new Warehouse(code, name)
        {
            Address = address,
            IsActive = isActive
        };
        return warehouse;
    }

    public void Update(string name, string? address, bool isActive)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Warehouse name is required.", nameof(name));
        Name = name.Trim();
        Address = address;
        IsActive = isActive;
    }
}
