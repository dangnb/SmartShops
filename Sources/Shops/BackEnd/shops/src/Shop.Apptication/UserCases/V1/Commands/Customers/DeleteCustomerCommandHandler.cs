using Microsoft.EntityFrameworkCore;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;

namespace Shop.Apptication.UserCases.V1.Commands.Customers;
public class DeleteCustomerCommandHandler : ICommandHandler<Command.DeleteCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IRepositoryBase<PaymentHistory, int> _paymentHistoryRepository;
    public DeleteCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, IRepositoryBase<PaymentHistory, int> paymentHistoryRepository)
    {
        _repositoryBase = repositoryBase;
        _paymentHistoryRepository = paymentHistoryRepository;
    }
    public async Task<Result> Handle(Command.DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repositoryBase.FindByIdAsync(request.Id)
            ?? throw new CustomersException.CustomerNotFoundException(request.Id);
        customer.PaymentHistories.Clear();
        //var paymentHistoris = await _paymentHistoryRepository.FindAll().Where(x => x.CustomerId == customer.Id).ToListAsync();
        //_paymentHistoryRepository.RemoveMultiple(paymentHistoris);
        _repositoryBase.Remove(customer);
        return Result.Success();
    }
}
