using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;

/// <summary>
/// Lịch sử thanh toán của khách hàng
/// </summary>
public class PaymentHistory : DomainEntity<int>
{
    public Guid CustomerId { get; private set; }
    public int ComId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public int Type { get; private set; }

    protected PaymentHistory() { }

    private PaymentHistory(int comId, Guid customerId, int quantity, decimal price, int type)
    {
        ComId = comId;
        CustomerId = customerId;
        Quantity = quantity;
        Price = price;
        Type = type;
    }
    public static PaymentHistory CreateEntity(int comId, Guid customerId, int quantity, decimal price, int type)
    {
        return new PaymentHistory(comId, customerId, quantity, price, type);
    }

    public void Update(int quantity, decimal price)
    {
        Quantity = quantity;
        Price = price;
    }
}
