namespace Shop.Contract.Services.V1.Payments.Dtos;
public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid Code { get; set; } 
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerCode { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string TotalOfMonth { get; set; } = string.Empty;
    public int NumberOfMonths { get; set; }
    public decimal Price { get;  set; }
    public decimal Total { get;  set; }
    public decimal VatAmount { get;  set; }
    public decimal Amount { get;  set; }
    public int Type { get;  set; }
    public int Status { get;  set; }
    public DateTime CreatedDate { get;  set; } = DateTime.Now;
    public string CreatedBy { get;  set; } = string.Empty;
    public DateTime? ModifiedDate { get;  set; }
    public string? ModifiedBy { get;  set; }
    public string Note { get;  set; } = string.Empty;
    public bool IsPrinted { get;  set; }
}
