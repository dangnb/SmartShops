namespace Shop.Contract.Services.V1.Purchasing.GoodsReceipts;

public static class Response
{
    public record GoodsReceiptResponse(Guid Id, string WarehouseName, string SupplierName, string ReceiptNo, int Status, decimal Subtotal, decimal Total, string CreatedAt, string CreatedBy);
}
