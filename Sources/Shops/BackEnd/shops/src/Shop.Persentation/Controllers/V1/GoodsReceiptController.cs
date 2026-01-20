using Asp.Versioning;
using HRM.Persentation.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Apptication.DTOs;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Purchasing.GoodsReceipts.Command;
using static Shop.Contract.Services.V1.Purchasing.GoodsReceipts.Query;

namespace Shop.Persentation.Controllers.V1;

[Authorize]
[ApiVersion(1)]
public class GoodsReceiptsController : ApiController
{
    private readonly ILogger<AccountsController> _logger;

    public GoodsReceiptsController(ISender sender, ILogger<AccountsController> logger) : base(sender: sender)
    {
        _logger = logger;
    }

    [HttpPost("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] GetGoodsReceiptsQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        }
            ;
        return Ok(result);
    }

    /// <summary>
    /// Tạo Phiếu Nhập Kho mới
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateGoodsReceipt([FromBody] CreateGoodsReceiptCommand cmd, CancellationToken ct)
    {
        var id = await sender.Send(cmd, ct);
        return Ok(new { id });
    }

    /// <summary>
    /// Thêm dòng vào Phiếu Nhập Kho
    /// </summary>
    [HttpPost("{id:guid}/lines")]
    public async Task<IActionResult> AddGoodsReceiptLine(Guid id, [FromBody] AddGoodsReceiptLineCommandBody body, CancellationToken ct)
    {
        await sender.Send(new AddGoodsReceiptLineCommand(id, body.ProductId, body.Qty, body.UnitCost), ct);
        return NoContent();
    }

    /// <summary>
    /// Đăng Phiếu Nhập Kho
    /// </summary>
    [HttpPost("{id:guid}/post")]
    public async Task<IActionResult> PostGoodsReceipt(Guid id, CancellationToken ct)
    {
        await sender.Send(new PostGoodsReceiptCommand(id), ct);
        return NoContent();
    }
}
