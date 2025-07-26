using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Reports;
using Shop.Domain.Entities.Identity;
using static Shop.Domain.Exceptions.RolesException;

namespace Shop.Apptication.UserCases.V1.Queries.Reports;
public class GetDataReportDetailQueryHandler : IQueryHandler<Query.GetDataReportDetailQuery,  PagedResult<Response.DataReportResponse>>
{
    public GetDataReportDetailQueryHandler()
    {
    }

    public Task<Result<PagedResult<Response.DataReportResponse>>> Handle(Query.GetDataReportDetailQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}