using Shop.Domain.Abstractions;
using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Order : EntityAuditBase<Guid>
{
    public Guid CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string Status { get; private set; } = string.Empty;
    protected Order() { }
    private Order(Guid comId, Guid customerId, decimal totalAmount, string status)
    {
        ComId = comId;
        CustomerId = customerId;
        TotalAmount = totalAmount;
        Status = status;
    }
    public static Order CreateEntity(Guid comId, Guid customerId, decimal totalAmount, string status)
    {
        return new Order(comId, customerId, totalAmount, status);
    }
    public void UpdateStatus(string status)
    {
        Status = status;
    }
}
