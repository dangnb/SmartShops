using MediatR;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Customers;

public class CreateCustomerCommandHandler : ICommandHandler<Command.CreateCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IPublisher _publisher;
    public CreateCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, IPublisher publisher)
    {
        _repositoryBase = repositoryBase;
        _publisher = publisher;
    }
    public async Task<Result> Handle(Command.CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Guid comId = Guid.Empty;
        var entity = Customer.CreateEntity(request.Code, request.Name, request.Address, request.Email, request.PhoneNumber, request.CitizenIdNumber, request.PassportNumber);
        if (_repositoryBase.FindSingleAsync(x => x.ComId == comId && x.Code == entity.Code) != null)
        {
            throw new InvalidOperationException();
        }
        _repositoryBase.Add(entity);
        return Result.Success(entity);
    }
}
