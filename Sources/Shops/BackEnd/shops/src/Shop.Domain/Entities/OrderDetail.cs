using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class OrderDetail : DomainEntity<Guid>, ICompanyScopedEntity
{
    public Guid ComId { get; private set; }
    public Guid OrderId { get; private set; } // Mã đơn hàng
    public Guid ProductId { get; private set; } // Mã sản phẩm
    public int Quantity { get; private set; } // Số lượng sản phẩm
    public decimal UnitPrice { get; private set; } // Giá bán đơn vị
    public decimal TotalPrice { get; private set; } // Tổng giá trị
    protected OrderDetail() { }
    private OrderDetail(Guid comId, Guid orderId, Guid productId, int quantity, decimal unitPrice, decimal totalPrice)
    {
        ComId = comId;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
    }
    public static OrderDetail CreateEntity(Guid comId, Guid orderId, Guid productId, int quantity, decimal unitPrice, decimal totalPrice)
    {
        return new OrderDetail(comId, orderId, productId, quantity, unitPrice, totalPrice);
    }
}
