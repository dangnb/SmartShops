using MediatR;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Companies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Companies;
public class CreateCompanyCommandHandler : ICommandHandler<Command.CreateCompanyCommand>
{
    private readonly IRepositoryBase<Company, int> _repositoryBase;
    private readonly IPublisher _publisher;
    public CreateCompanyCommandHandler(IRepositoryBase<Company, int> repositoryBase, IPublisher publisher)
    {
        _repositoryBase = repositoryBase;
        _publisher = publisher;
    }
    public async Task<Result> Handle(Command.CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = Company.CreateEntity(request.Code, request.Name, request.Addess, request.Phone, request.Mail, request.NumberAccount);
        _repositoryBase.Add(entity);
        await _publisher.Publish(new DomainEvent.CompanyCreated(entity.Id, entity.Code, "admin"));
        return Result.Success(entity);
    }
}
