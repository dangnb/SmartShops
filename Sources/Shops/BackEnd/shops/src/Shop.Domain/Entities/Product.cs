using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;
using Shop.Domain.Entities.enums;

namespace Shop.Domain.Entities;
/// <summary>
/// Bảng sản phẩm
/// </summary>
public class Product : DomainEntity<Guid>, ICompanyScopedEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string BarCode { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; } // Loại sản phẩm
    public decimal UnitPrice { get; private set; } //Giá bán
    public decimal CostPrice { get; private set; } //Giá nhập gần nhất
    public int StockQuantity { get; private set; } // Tổng tồn kho
    public string ImageUrl { get; private set; } = string.Empty; // Đường dẫn hình ảnh
    public ProductStatus Status { get; private set; } = ProductStatus.New; // Trạng thái sản phẩm
    public Guid ComId { get;  set; }

    protected Product() { }

    private Product(Guid comId, string name, string code, string barCode, Guid categoryId, decimal unitPrice, decimal costPrice, int stockQuantity, string imageUrl)
    {
        ComId = comId;
        Name = name;
        Code = code;
        BarCode = barCode;
        CategoryId = categoryId;
        UnitPrice = unitPrice;
        CostPrice = costPrice;
        StockQuantity = stockQuantity;
        ImageUrl = imageUrl;
    }
    public static Product CreateEntity(Guid comId, string name, string code, string barCode, Guid categoryId, decimal unitPrice, decimal costPrice, int stockQuantity, string imageUrl)
    {
        return new Product(comId, name, code, barCode, categoryId, unitPrice, costPrice, stockQuantity, imageUrl);
    }

    public void Update(string name, decimal unitPrice, decimal costPrice)
    {
        Name = name;
        UnitPrice = unitPrice;
        CostPrice = costPrice;
    }
}
