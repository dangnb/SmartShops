using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Purchasing.GoodsReceipts;

public static class Query
{
    public record GetGoodsReceiptsQuery(Guid? SupplierId, Guid? WarehouseId, string FromDate, string ToDate, int? Status, int PageIndex, int PageSize) : IQuery<PagedResult<Response.GoodsReceiptResponse>>;
}
