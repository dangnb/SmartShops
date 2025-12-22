using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;
using Shop.Domain.Entities.enums;

namespace Shop.Domain.Entities;
/// <summary>
/// Bảng sản phẩm
/// </summary>
public class Product : EntityAuditBase<Guid>, ICompanyScopedEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string BarCode { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; } // Loại sản phẩm
    public ProductStatus Status { get; private set; } = ProductStatus.New; // Trạng thái sản phẩm
    protected Product() { }

    private Product(Guid comId, string name, string code, string barCode, Guid categoryId)
    {
        ComId = comId;
        Name = name;
        Code = code;
        BarCode = barCode;
        CategoryId = categoryId;
    }
    public static Product CreateEntity(Guid comId, string name, string code, string barCode, Guid categoryId)
    {
        return new Product(comId, name, code, barCode, categoryId);
    }

    public void Update(string name)
    {
        Name = name;
    }
}
