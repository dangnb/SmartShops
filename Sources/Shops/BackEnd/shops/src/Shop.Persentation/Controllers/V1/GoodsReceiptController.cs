using Asp.Versioning;
using HRM.Persentation.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Apptication.DTOs;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.Purchasing.V1.GoodsReceipts.Command;

namespace Shop.Persentation.Controllers.V1;

[ApiVersion(1)]
public class GoodsReceiptController : ApiController
{
    private readonly ILogger<AccountsController> _logger;

    public GoodsReceiptController(ISender sender, ILogger<AccountsController> logger) : base(sender: sender)
    {
        _logger = logger;
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
