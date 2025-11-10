using MediatR;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CustomersException;

namespace Shop.Apptication.UserCases.V1.Commands.Customers;

public class DeleteCustomerCommandHandler : ICommandHandler<Command.DeleteCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IPublisher _publisher;
    private readonly ICurrentUser _currentUser;
    public DeleteCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, IPublisher publisher, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _publisher = publisher;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        var entity = await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Id == request.Id) ?? throw new CustomerNotFoundException(request.Id);
        _repositoryBase.Remove(entity);
        return Result.Success(entity);
    }
}
