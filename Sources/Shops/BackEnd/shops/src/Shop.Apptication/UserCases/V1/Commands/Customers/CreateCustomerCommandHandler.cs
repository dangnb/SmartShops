using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Customers;
public class CreateCustomerCommandHandler : ICommandHandler<Command.CreateCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IRepositoryBase<PaymentHistory, int> _repositoryPaymentHistory;
    private readonly IUserProvider _userProvider;
    public CreateCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, IRepositoryBase<PaymentHistory, int> repositoryPaymentHistory, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
        _repositoryPaymentHistory = repositoryPaymentHistory;
    }
    public async Task<Result> Handle(Command.CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = Customer.CreateEntity(_userProvider.GetComID(), request.Code, request.Name, request.Address, request.Email, request.PhoneNumber, request.VillageId);
        _repositoryBase.Add(entity);

        //Tạo lịch sử thanh toán cho khách hàng
        foreach (var item in request.payments)
        {
            if (item.price > 0 && item.quantity > 0)
            {
                var paymentHistory = PaymentHistory.CreateEntity(_userProvider.GetComID(), entity.Id, item.quantity, item.price, item.type);
                _repositoryPaymentHistory.Add(paymentHistory);
            }
        }

        return Result.Success(entity);
    }
}
