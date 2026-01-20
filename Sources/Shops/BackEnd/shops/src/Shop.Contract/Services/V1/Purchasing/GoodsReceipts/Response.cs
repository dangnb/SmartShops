namespace Shop.Contract.Services.V1.Purchasing.GoodsReceipts;

public class Response
{
    public record GoodsReceiptResponse(Guid Id, string WarehouseName, string SupplierName, string ReceiptNo, int Status, decimal Subtotal, decimal Total, DateTime CreatedAt, string CreatedBy);
}
