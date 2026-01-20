using Asp.Versioning;
using HRM.Persentation.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Apptication.DTOs;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.V1.Purchasing.Invoices.Command;

namespace Shop.Persentation.Controllers.V1;

[ApiVersion(1)]
public class PurchaseInvoiceController : ApiController
{
    private readonly ILogger<AccountsController> _logger;

    public PurchaseInvoiceController(ISender sender, ILogger<AccountsController> logger) : base(sender: sender)
    {
        _logger = logger;
    }
    /// <summary>
    /// Tạo Hóa Đơn Mua
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreatePurchaseInvoice([FromBody] CreatePurchaseInvoiceCommand cmd, CancellationToken ct)
    {
        var id = await sender.Send(cmd, ct);
        return Ok(new { id });
    }

    /// <summary>
    /// Thêm dòng vào Hóa Đơn Mua
    /// </summary>
    [HttpPost("{id:guid}/lines")]
    public async Task<IActionResult> AddInvoiceLine(Guid id, [FromBody] AddInvoiceLineCommand body, CancellationToken ct)
    {
        await sender.Send(new AddInvoiceLineCommand(id, body.ProductId, body.Qty, body.UnitPrice, body.TaxRate), ct);
        return NoContent();
    }

    /// <summary>
    /// Phát hành Hóa Đơn Mua
    /// </summary>
    [HttpPost("{id:guid}/issue")]
    public async Task<IActionResult> IssueInvoice(Guid id, CancellationToken ct)
    {
        await sender.Send(new IssueInvoiceCommand(id), ct);
        return NoContent();
    }

    /// <summary>
    /// Khớp Hóa Đơn Mua với Phiếu Nhập
    /// </summary>
    [HttpPost("{id:guid}/match")]
    public async Task<IActionResult> MatchInvoice(Guid id, [FromBody] MatchInvoiceBody body, CancellationToken ct)
    {
        var items = body.Items.Select(x => new MatchItem(x.ReceiptId, x.ProductId, x.QtyMatched, x.AmountMatched, x.ReceiptLineId)).ToList();
        await sender.Send(new MatchInvoiceToReceiptsCommand(id, items), ct);
        return NoContent();
    }
}
