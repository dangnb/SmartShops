namespace Shop.Apptication.DTOs;

public class GoodsReceiptViewDto
{
    public Guid Id { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string ReceiptNo { get; set; } = string.Empty;
    public int Status { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
