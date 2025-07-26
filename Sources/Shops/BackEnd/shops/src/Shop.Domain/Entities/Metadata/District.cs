using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities.Metadata;
public class District : DomainEntity<int>
{
    public int CityId { get; private set; }
    public int ComId { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public virtual City City { get; private set; }

    protected District() { }
    private District(int comId, int cityId, string code, string name)
    {
        ComId = comId;
        Code = code;
        Name = name;
        CityId = cityId;
    }

    public static District CreateEntity(int comId, int cityId, string code, string name) { return new District(comId, cityId, code, name); }

    public void Update(int cityId, string code, string name, City city) { Code = code; Name = name; CityId = cityId;

        City = city; }

}
