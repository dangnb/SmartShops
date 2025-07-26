using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities.Metadata;
public class Ward : DomainEntity<int>
{
    public int DistrictId { get; private set; }
    public int ComId { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public virtual District District { get; private set; }

    protected Ward() { }
    private Ward(int comId, int districtId, string code, string name)
    {
        ComId = comId;
        Code = code;
        Name = name;
        DistrictId = districtId;
    }

    public static Ward CreateEntity(int comId, int districtId, string code, string name) { return new Ward(comId, districtId, code, name); }

    public void Update(int districtId, string code, string name) { Code = code; Name = name; DistrictId = districtId; }
}
