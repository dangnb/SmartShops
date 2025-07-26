using System.Text;
using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Payments;
using Shop.Contract.Services.V1.Payments.Dtos;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Apptication.UserCases.V1.Queries.Payments;
internal class GetPaymentsQueryHandler : IQueryHandler<Query.GetPaymentsQuery, PagedResult<Response.PaymentResponse>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetPaymentsQueryHandler(IPaymentRepository paymentRepository, IMapper mapper, IUserProvider userProvider)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.PaymentResponse>>> Handle(Query.GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        var comId = _userProvider.GetComID();
        StringBuilder query = new StringBuilder("SELECT pay.id, pay.Code, ");
        query.Append(" pay.CustomerId AS CustomerId, ");
        query.Append(" pay.Quantity, pay.TotalOfMonth, ");
        query.Append(" pay.NumberOfMonths, ");
        query.Append(" pay.Price, ");
        query.Append(" pay.Total, ");
        query.Append(" pay.VatAmount, ");
        query.Append(" pay.Amount, ");
        query.Append(" pay.Type, ");
        query.Append(" pay.Status, ");
        query.Append(" pay.CreatedDate, ");
        query.Append(" pay.CreatedBy, ");
        query.Append(" pay.ModifiedDate, ");
        query.Append(" pay.ModifiedBy, ");
        query.Append(" pay.Note, ");
        query.Append(" pay.IsPrinted, ");
        query.Append(" cus.Name AS CustomerName, ");
        query.Append(" cus.Code AS CustomerCode, ");
        query.Append(" cus.Address AS CustomerAddress ");

        query.Append("FROM payments AS pay ");
        query.Append("JOIN  customers AS cus ON pay.CustomerId = cus.Id ");

        query.Append("where pay.ComId = @comid ");

        List<Domain.Common.SQLParam> sQLParams = new List<Domain.Common.SQLParam>();
        sQLParams.Add(new Domain.Common.SQLParam("comid", comId.ToString()));
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query.Append(" and (cus.Name like @searchTerm or cus.Code like @searchTerm)");
            sQLParams.Add(new Domain.Common.SQLParam("searchTerm", $"{request.SearchTerm}%"));
        }

        var (payments, total) = await _paymentRepository.GetDynamicPagedAsync<PaymentDto>(query.ToString(), request.PageIndex, request.PageSize, sQLParams.ToArray());
        var page = PagedResult<PaymentDto>.Create(payments.ToList(), request.PageIndex, request.PageSize, total);
        var result = _mapper.Map<PagedResult<Response.PaymentResponse>>(page);
        return Result.Success(result);
    }
}
