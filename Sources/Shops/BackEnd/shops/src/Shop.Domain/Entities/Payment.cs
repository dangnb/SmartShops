using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class Payment : DomainEntity<Guid>
{
    public int ComId { get; private set; }
    public Guid Code { get; private set; }//Mã thanh toán
    public Guid CustomerId { get; private set; }
    public int Quantity { get; private set; }
    public string TotalOfMonth { get; private set; } = string.Empty;
    public int NumberOfMonths { get; private set; }
    public decimal Price { get; private set; }
    public decimal Total { get; private set; }
    public decimal VatAmount { get; private set; }
    public decimal Amount { get; private set; }
    public int Type { get; private set; }
    public int Status { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTime? ModifiedDate { get; private set; }
    public string? ModifiedBy { get; private set; }
    public string Note { get; private set; }
    public bool IsPrinted { get; private set; }
    public virtual ICollection<PaymentDetail> InvoiceDetails { get; private set; }

    protected Payment() { }

    private Payment(int comId, Guid code, Guid customerId, int quantity, string totalOfMonth, int numberOfMonths,
        decimal price, decimal total, decimal vatAmount, decimal amount, int type, string createdBy, string note)
    {
        Code = code;
        ComId = comId;
        Code = code;
        CustomerId = customerId;
        Quantity = quantity;
        TotalOfMonth = totalOfMonth;
        NumberOfMonths = numberOfMonths;
        Price = price;
        Total = total;
        VatAmount = vatAmount;
        Amount = amount;
        Type = type;
        CreatedDate = DateTime.Now;
        CreatedBy = createdBy;
        Note = note;

    }
    public static Payment CreateEntity(int comId, Guid code, Guid customerId, int quantity, string totalOfMonth, int numberOfMonths,
        decimal price, decimal total, decimal vatAmount, decimal amount, int type, string createdBy, string note)
    {
        return new Payment(comId, code, customerId, quantity, totalOfMonth, numberOfMonths,
         price, total, vatAmount, amount, type, createdBy, note);
    }
}
