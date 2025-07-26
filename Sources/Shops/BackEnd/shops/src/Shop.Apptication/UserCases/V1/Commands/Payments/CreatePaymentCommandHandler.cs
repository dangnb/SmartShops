using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Payments;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using static Shop.Domain.Exceptions.CustomersException;

namespace Shop.Apptication.UserCases.V1.Commands.Payments;
/// <summary>
/// Xử lý thanh toán
/// </summary>
public class CreatePaymentCommandHandler : ICommandHandler<Command.CreatePaymentCommand>
{
    private readonly IRepositoryBase<Payment, Guid> _repositoryPayment;
    private readonly IRepositoryBase<PaymentDetail, int> _repositoryPaymentDetail;
    private readonly IRepositoryBase<Customer, Guid> _repositoryCustomer;
    private readonly IRepositoryBase<PaymentSummary, int> _repositoryPaymentSummary;
    private readonly IRepositoryBase<PaymentHistory, int> _repositoryPaymentHistory;
    private readonly IUserProvider _userProvider;
    public CreatePaymentCommandHandler(IRepositoryBase<Payment, Guid> repositoryInvoice, IRepositoryBase<PaymentDetail, int> repositoryInvoiceDetail
        , IRepositoryBase<Customer, Guid> repositoryCustomer
        , IRepositoryBase<PaymentSummary, int> repositoryProgressPaymentCustomer
        , IRepositoryBase<PaymentHistory, int> repositoryPaymentHistory
        , IUserProvider userProvider)
    {
        _repositoryPayment = repositoryInvoice;
        _userProvider = userProvider;
        _repositoryPaymentDetail = repositoryInvoiceDetail;
        _repositoryPaymentSummary = repositoryProgressPaymentCustomer;
        _repositoryCustomer = repositoryCustomer;
        _repositoryPaymentHistory = repositoryPaymentHistory;
    }
    public async Task<Result> Handle(Command.CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var comId = _userProvider.GetComID();
        var username = _userProvider.GetUserName();
        var paymentCode = Guid.NewGuid();
        //check exist customer
        if (!await _repositoryCustomer.FindAll().AnyAsync(x => x.Id == request.CustomerId))
        {
            throw new CustomerNotFoundException(request.CustomerId);
        }
        foreach (var paymentOfMonth in request.PaymentOfMonths)
        {
            int[] months = paymentOfMonth.Month.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            var total = paymentOfMonth.Quantity * paymentOfMonth.Price * months.Length;

            //Tạo hóa đơn
            var payment = Payment.CreateEntity(comId, paymentCode, request.CustomerId, 
                                                paymentOfMonth.Quantity, paymentOfMonth.Month, 
                                                months.Length, paymentOfMonth.Price, total, 0, 
                                                total, paymentOfMonth.Type, username, paymentOfMonth.Note);


            _repositoryPayment.Add(payment);

            //Tạo chi tiết hóa đơn
            var paymentDetail = PaymentDetail.CreateEntity(comId, payment.Id, paymentOfMonth.Quantity, paymentOfMonth.Month, months.Length, 0, paymentOfMonth.Price, total, 0, total);
            _repositoryPaymentDetail.Add(paymentDetail);

            //Cập nhật bảng thu năm của khách hàng
            var paymentSummary = await _repositoryPaymentSummary.FindSingleAsync(x => x.ComId == comId && x.CustomerId == request.CustomerId &&
                                                                            x.Type == payment.Type && x.Year == payment.CreatedDate.Year);
            if (paymentSummary != null)
            {
                var monthsOld = paymentSummary.PaidOfMonth.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                if (monthsOld.Any(x => months.Contains(x)))
                {
                    throw new PaymentException("Thanh toán không thành công. Thanh tán tồn tại tháng đã được thanh toán trước đó");
                }
                var paidOfMonthNew = string.Join(",", monthsOld) + "," + string.Join(",", months);
                paymentSummary.Update(paymentSummary.NumberOfYear + months.Length, paidOfMonthNew, paymentSummary.Quantity + payment.Quantity, payment.Amount + paymentSummary.Total);
                if (paymentSummary.NumberOfYear > 12)
                {
                    throw new PaymentException("Thanh toán không thành công. Số lượng tháng thanh toán vượt quá số lượng cho phép");
                }
                _repositoryPaymentSummary.Update(paymentSummary);
            }
            else
            {
                paymentSummary = PaymentSummary.CreateEntity(comId, request.CustomerId, payment.CreatedDate.Year, months.Length, string.Join(",", months), payment.Quantity, payment.Amount, paymentOfMonth.Type);
                _repositoryPaymentSummary.Add(paymentSummary);
            }

            //Cập nhật PaymentHistory
            var paymentHistory = await _repositoryPaymentHistory.FindSingleAsync(x => x.ComId == comId && x.CustomerId == payment.CustomerId && x.Type == payment.Type);
            if (paymentHistory != null)
            {
                paymentHistory.Update(paymentOfMonth.Quantity, paymentOfMonth.Price);
                _repositoryPaymentHistory.Update(paymentHistory);
            }
            else
            {
                paymentHistory = PaymentHistory.CreateEntity(comId, payment.CustomerId, paymentOfMonth.Quantity, paymentOfMonth.Price, payment.Type);
                _repositoryPaymentHistory.Add(paymentHistory);
            }

        }
        return Result.Success();

    }
}
