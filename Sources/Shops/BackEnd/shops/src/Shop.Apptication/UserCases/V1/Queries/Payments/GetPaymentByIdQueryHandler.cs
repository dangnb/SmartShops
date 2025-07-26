using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Payments;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Apptication.UserCases.V1.Queries.Payments;
public class GetPaymentByIdQueryHandler : IQueryHandler<Query.GetPaymentByIdQuery, Response.PaymentResponse>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository, IMapper mapper) { _paymentRepository = paymentRepository; _mapper = mapper; }

    public async Task<Result<Response.PaymentResponse>> Handle(Query.GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync("Payments", request.ID);
        return Result.Success(_mapper.Map<Response.PaymentResponse>(payment));
    }
}
