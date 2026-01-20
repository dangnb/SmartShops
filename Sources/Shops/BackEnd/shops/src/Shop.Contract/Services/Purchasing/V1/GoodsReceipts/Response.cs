namespace Shop.Contract.Services.Purchasing.V1.GoodsReceipts;

public class Response
{
    public record GoodsReceiptResponse(Guid Id, string WarehouseName, string SupplierName, string ReceiptNo, int Status, decimal Subtotal, decimal Total, DateTime CreatedAt, string CreatedBy);
}
