using Asp.Versioning;
using HRM.Persentation.Controllers.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Apptication.DTOs;
using Shop.Persentation.Abtractions;
using static Shop.Contract.Services.Payables.V1.Payments.Command;

namespace Shop.Persentation.Controllers.V1;

[ApiVersion(1)]
public class PaymentController : ApiController
{
    private readonly ILogger<AccountsController> _logger;

    public PaymentController(ISender sender, ILogger<AccountsController> logger) : base(sender: sender)
    {
        _logger = logger;
    }
    /// <summary>
    /// Ghi nhận Thanh Toán
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> RecordPayment([FromBody] RecordPaymentCommand cmd, CancellationToken ct)
    {
        var id = await sender.Send(cmd, ct);
        return Ok(new { id });
    }

    /// <summary>
    /// Phân bổ Thanh Toán cho Hóa Đơn
    /// </summary>
    [HttpPost("{id:guid}/allocate")]
    public async Task<IActionResult> AllocatePayment(Guid id, [FromBody] AllocatePaymentBody body, CancellationToken ct)
    {
        await sender.Send(new AllocatePaymentCommand(id, body.InvoiceId, body.Amount), ct);
        return NoContent();
    }
}
