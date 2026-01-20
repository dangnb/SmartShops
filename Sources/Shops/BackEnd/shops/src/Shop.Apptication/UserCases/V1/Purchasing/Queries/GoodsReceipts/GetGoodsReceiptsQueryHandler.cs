using System.Text;
using AutoMapper;
using Shop.Apptication.DTOs;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Purchasing.GoodsReceipts;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Apptication.UserCases.V1.Purchasing.Queries.GoodsReceipts;
public class GetGoodsReceiptsQueryHandler : IQueryHandler<Query.GetGoodsReceiptsQuery, PagedResult<Response.GoodsReceiptResponse>>
{
    private readonly IGoodReceiptRepository _goodReceiptRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public GetGoodsReceiptsQueryHandler(IGoodReceiptRepository goodReceiptRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _goodReceiptRepository = goodReceiptRepository;
        _mapper = mapper;
        _currentUser = userProvider;
    }

    public async Task<Result<PagedResult<Response.GoodsReceiptResponse>>> Handle(Query.GetGoodsReceiptsQuery request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        var query = new StringBuilder("SELECT");
        query.Append(" wh.Name as  WarehouseName, ");
        query.Append(" s.Name as SupplierName, ");
        query.Append(" gr.ReceiptNo, ");
        query.Append(" gr.Id, ");
        query.Append(" gr.Status, ");
        query.Append(" gr.Subtotal, ");
        query.Append(" gr.Total, ");
        query.Append(" gr.CreatedAt, ");
        query.Append(" gr.CreatedBy  ");
        query.Append(" from goods_receipts gr ");
        query.Append(" join suppliers s on  gr.SupplierId  = s.Id ");
        query.Append(" join warehouses wh on  gr.WarehouseId  = wh.Id ");
        query.Append("where gr.ComId = @comid ");
        List<Domain.Common.SQLParam> sQLParams = [new Domain.Common.SQLParam("comid", comId.ToString())];

        var (payments, total) = await _goodReceiptRepository.GetDynamicPagedAsync<GoodsReceiptViewDto>(query.ToString(), request.PageIndex, request.PageSize, sQLParams.ToArray());
        var page = PagedResult<GoodsReceiptViewDto>.Create(payments.ToList(), request.PageIndex, request.PageSize, total);
        var result = _mapper.Map<PagedResult<Response.GoodsReceiptResponse>>(page);
        return Result.Success(result);
    }
}
