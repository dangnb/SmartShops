using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
/// <summary>
/// Bảng sản phẩm
/// </summary>
public class Product : DomainEntity<int>
{
    public int ComId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public int ProductType { get; private set; } //0 là giá khách lẻ - 1: Hộ kinh doanh 

    protected Product() { }

    private Product(int comId, string name, string code, decimal price, bool isActive, int productType)
    {
        ComId = comId;
        Name = name;
        Code = code;
        Price = price;
        IsActive = isActive;
        ProductType = productType;
    }
    public static Product CreateEntity(int comId, string name, string code, decimal price, bool isActive, int productType)
    {
        return new Product(comId, name, code, price, isActive, productType);
    }

    public void Update(string name, string code, decimal price, bool isActive, int productType)
    {
        Name = name;
        Code = code;
        Price = price;
        IsActive = isActive;
        ProductType = productType;
    }
}
