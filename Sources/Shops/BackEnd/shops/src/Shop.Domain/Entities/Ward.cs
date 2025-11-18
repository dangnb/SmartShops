using Shop.Domain.Abstractions;

namespace Shop.Domain.Entities;

public class Ward : EntityAuditBase<Guid>
{
    public Guid ProvinceId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public virtual Province Province { get; set; } = default!;

    protected Ward() { }
    private Ward(string code, string name, Guid provinceId)
    {
        Code = code;
        Name = name;
        ProvinceId = provinceId;
    }
    public static Ward CreateEntity(string code, string name, Guid provinceId)
    {
        return new Ward(code, name, provinceId);
    }
    public void Update(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
