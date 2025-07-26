using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Reports;
public static class Query
{
    public record GetDataReportDetailQuery(string? keyword, int? districtId,int? wardId, int? villageId,string fromDate, string toDate,  int PageIndex, int PageSize) : IQuery<PagedResult<Response.DataReportResponse>>;
}
