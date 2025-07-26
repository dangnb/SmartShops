using System.Text;
using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Dappers.Repositories;
using Shop.Domain.Entities;
using static Shop.Contract.Services.V1.Customers.Response;
using static Shop.Domain.Common;

namespace Shop.Apptication.UserCases.V1.Queries.Customers;
public class GetCustomersPaidQueryHandler : IQueryHandler<Query.GetCustomersPaidQuery, PagedResult<CustomerResponse>>
{
    private readonly ICustomerRepository _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;

    public GetCustomersPaidQueryHandler(ICustomerRepository repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<CustomerResponse>>> Handle(Query.GetCustomersPaidQuery request, CancellationToken cancellationToken)
    {
        var comId = _userProvider.GetComID();
        StringBuilder queryString = new StringBuilder("SELECT c.* ");

        queryString.Append("FROM customers c ");
        queryString.Append("LEFT JOIN (SELECT * FROM  paymentsummaries WHERE  comid= @comid AND YEAR = @year) hc ");
        queryString.Append(" ON c.Id = hc.CustomerId ");
        queryString.Append("WHERE hc.Id IS NULL  AND c.ComId = @comid ");
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            queryString.Append("AND (c.Code LIKE @searchTerm or c.Name LIKE @searchTerm");
        }
        queryString.Append("UNION ALL ");
        queryString.Append("SELECT c.* ");
        queryString.Append("FROM customers c ");
        queryString.Append("LEFT JOIN (SELECT * FROM  paymentsummaries WHERE  comid= @comid AND YEAR = @year) hc ON c.Id = hc.CustomerId ");
        queryString.Append("WHERE  c.ComId = @comid ");
        queryString.Append("AND hc.NumberOfYear < @month ");


        List<SQLParam> sQLParams = new List<SQLParam>()
        {
            new ("comid", comId.ToString()),
            new ("year", request.Year.ToString()),
            new ("month", "12"),
        };

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            sQLParams.Add(new SQLParam("searchTerm", $"{request.SearchTerm}%"));
        }
        var (customers, total) = await _repositoryBase.GetDynamicPagedAsync<Customer>(queryString.ToString(), request.PageIndex, request.PageSize, sQLParams.ToArray());
        var items = _mapper.Map<List<CustomerResponse>>(customers);
        return Result.Success(PagedResult<CustomerResponse>.Create(items, request.PageIndex, request.PageSize, total));
    }
}
