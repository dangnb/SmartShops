using MediatR;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Customers;

public class CreateCustomerCommandHandler : ICommandHandler<Command.CreateCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IPublisher _publisher;
    private readonly ICurrentUser _currentUser;
    public CreateCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, IPublisher publisher, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _publisher = publisher;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        var entity = Customer.CreateEntity(comId, request.Code, request.Name, request.Address, request.Email, request.PhoneNumber, request.CitizenIdNumber, request.PassportNumber);
        if (await _repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Code == entity.Code) != null)
        {
            throw new InvalidOperationException();
        }
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
