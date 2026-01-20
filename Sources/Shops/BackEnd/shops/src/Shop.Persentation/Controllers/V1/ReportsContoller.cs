using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Contract.Services.V1.Common.Reports;
using Shop.Persentation.Abtractions;

namespace Shop.Persentation.Controllers.V1;
[ApiVersion(1)]
public class ReportsContoller(ISender sender) : ApiController(sender)
{

    #region báo cáo chi tiết thanh toán start

    [Authorize]
    [HttpPost("ReportDetail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Filter([FromBody] Query.GetDataReportDetailQuery request)
    {
        var result = await sender.Send(request);
        if (result.IsFailure)
        {
            return HandlerFailure(result);
        };
        return Ok(result);
    }
    #endregion end

    #region báo cáo thu theo nhân viên
    #endregion end

    #region Thống kê trạng thái thanh toán
    #endregion end

}
