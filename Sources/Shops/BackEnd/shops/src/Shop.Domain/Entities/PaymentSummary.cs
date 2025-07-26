using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
/// <summary>
/// 
/// </summary>
public class PaymentSummary : DomainEntity<int>
{
    public int ComId { get; private set; }
    public Guid CustomerId { get; private set; }
    public int Year { get; private set; }
    public int NumberOfYear { get; private set; }
    public string PaidOfMonth { get; private set; }
    public DateTime LastDate { get; private set; }

    /// <summary>
    /// Tổng số lượng nhân khẩu trên năm
    /// </summary>
    public int Quantity { get; private set; }
    /// <summary>
    /// Tổng tiền thu đương trên năm
    /// </summary>
    public decimal Total { get; private set; }
    public int Type { get; private set; }

    protected PaymentSummary() { }

    private PaymentSummary(int comId, Guid customerId, int year, int numberOfYear, string paidOfMonth, int quantity, decimal total, int type)
    {
        ComId = comId;
        CustomerId = customerId;
        Year = year;
        NumberOfYear = numberOfYear;
        LastDate = DateTime.Now;
        Type = type;
        PaidOfMonth = paidOfMonth;
        Quantity = quantity;
        Total = total;

    }
    public static PaymentSummary CreateEntity(int comId, Guid customerId, int year, int numberOfYear, string paidOfMonth, int quantity, decimal total, int type)
    {
        return new PaymentSummary(comId, customerId, year, numberOfYear, paidOfMonth, quantity, total, type);
    }

    public void Update(int numberOfYear, string paidOfMonth, int quantity, decimal total)
    {
        NumberOfYear = numberOfYear;
        LastDate = DateTime.Now;
        PaidOfMonth = paidOfMonth;
        Quantity = quantity;
        Total = total;
    }
}
