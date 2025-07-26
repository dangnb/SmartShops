using Shop.Domain.Abstractions.Entities;

namespace Shop.Domain.Entities;
public class PaymentDetail : DomainEntity<int>
{
    public int ComId { get; private set; }
    public Guid PaymentId { get; private set; }
    public int Quantity { get; private set; }
    public string TotalOfMonths { get; private set; } = string.Empty;
    public int NumberOfMonths { get; private set; }
    public float VatRate { get; private set; }
    public decimal Price { get; private set; }
    public decimal Total { get; private set; }
    public decimal VatAmount { get; private set; }
    public decimal Amount { get; private set; }

    protected PaymentDetail() { }

    private PaymentDetail(int comId, Guid paymentId, int quantity
        , string totalOfMonths, int numberOfMonths, float vatRate
        , decimal price, decimal total, decimal vatAmount, decimal amount)
    {
        ComId = comId;
        PaymentId = paymentId;
        Quantity = quantity;
        Total = total;
        NumberOfMonths = numberOfMonths;
        VatRate = vatRate;
        Price = price;
        Total = total;
        VatAmount = vatAmount;
        Amount = amount;
        TotalOfMonths = totalOfMonths;
    }
    public static PaymentDetail CreateEntity(
        int comId, Guid invoiceId, int quantity
        , string totalOfMonths, int numberOfMonths, float vatRate
        , decimal price, decimal total, decimal vatAmount, decimal amount)
    {
        return new PaymentDetail(comId, invoiceId, quantity
        , totalOfMonths, numberOfMonths, vatRate
        , price, total, vatAmount, amount);
    }

    public void Update(int quantity
        , string totalOfMonths, int numberOfMonths, float vatRate
        , decimal price, decimal total, decimal vatAmount, decimal amount)
    {
        Quantity = quantity;
        TotalOfMonths = totalOfMonths;
        NumberOfMonths = numberOfMonths;
        VatRate = vatRate;
        Price = price;
        Total = total;
        VatAmount = vatAmount;
        Amount = amount;
    }
}
