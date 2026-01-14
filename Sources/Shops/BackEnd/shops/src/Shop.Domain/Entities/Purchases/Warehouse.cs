using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities.Purchases;

public class Warehouse: EntityAuditBase<Guid>
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Address { get; private set; }
    public bool IsActive { get; private set; } = true;
    private Warehouse() { }

    public Warehouse(string code, string name)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Warehouse code is required.", nameof(code));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Warehouse name is required.", nameof(name));
        Code = code.Trim();
        Name = name.Trim();
    }
}
