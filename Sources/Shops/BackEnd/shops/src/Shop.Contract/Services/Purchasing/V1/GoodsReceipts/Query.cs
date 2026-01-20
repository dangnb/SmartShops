using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.Purchasing.V1.GoodsReceipts;

public class Query
{
    public record GetCategoriesQuery(Guid? SupplierId, Guid? WarehouseId, string FromDate, string ToDate, int? Status, int PageIndex, int PageSize) : IQuery<PagedResult<Response.GoodsReceiptResponse>>;
}
