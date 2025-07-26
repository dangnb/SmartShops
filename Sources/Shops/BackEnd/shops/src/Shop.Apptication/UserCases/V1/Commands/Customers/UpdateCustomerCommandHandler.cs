using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.CustomersException;
using static Shop.Domain.Exceptions.VillagesException;

namespace Shop.Apptication.UserCases.V1.Commands.Customers;
public class UpdateCustomerCommandHandler : ICommandHandler<Command.UpdateCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IRepositoryBase<Village, int> _villageRepository;
    private readonly IRepositoryBase<PaymentHistory, int> _paymentHistoryRepository;
    private readonly IUserProvider _userProvider;
    public UpdateCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, IRepositoryBase<PaymentHistory, int> paymentHistoryRepository, IRepositoryBase<Village, int> villageRepository, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
        _villageRepository = villageRepository;
        _paymentHistoryRepository = paymentHistoryRepository;
    }
    public async Task<Result> Handle(Command.UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var village = await _villageRepository.FindByIdAsync(request.VillageId)
            ?? throw new VillageNotFoundException(request.VillageId);
        var customer = await _repositoryBase.FindByIdAsync(request.Id)
            ?? throw new CustomerNotFoundException(request.Id);
        customer.PaymentHistories.Clear();
        //Tạo lịch sử thanh toán cho khách hàng
        foreach (var item in request.payments)
        {
            if (item.price > 0 && item.quantity > 0)
            {
                var paymentHistory = PaymentHistory.CreateEntity(_userProvider.GetComID(), customer.Id, item.quantity, item.price, item.type);
                _paymentHistoryRepository.Add(paymentHistory);
            }
        }

        customer.Update(request.Code, request.Name, request.Address, request.Email, request.PhoneNumber, request.VillageId);


        _repositoryBase.Update(customer);
        return Result.Success(customer);
    }
}
