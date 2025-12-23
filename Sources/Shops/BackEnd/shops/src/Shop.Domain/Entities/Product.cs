using DocumentFormat.OpenXml.Wordprocessing;
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

    private Product(string name, string code, string barCode, Guid categoryId)
    {
        Name = name;
        Code = code;
        BarCode = barCode;
        CategoryId = categoryId;
    }
    public static Product CreateEntity(string name, string code, string barCode, Guid categoryId)
    {
        return new Product(name, code, barCode, categoryId);
    }

    public void Update(string name, string barCode, Guid categoryId)
    {
        Name = name;
        BarCode = barCode;
        CategoryId = categoryId;
    }
    public void SoftDelete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            Status = ProductStatus.Inactive;
        }
    }
}
