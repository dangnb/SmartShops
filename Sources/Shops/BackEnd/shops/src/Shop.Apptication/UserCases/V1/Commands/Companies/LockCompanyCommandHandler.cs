using MediatR;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Companies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Commands.Companies;
public class LockCompanyCommandHandler : ICommandHandler<Command.LockCompanyCommandHandler>
{
    private readonly IRepositoryBase<Company, int> _repositoryBase;
    public LockCompanyCommandHandler(IRepositoryBase<Company, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.LockCompanyCommandHandler request, CancellationToken cancellationToken)
    {
        var company = await _repositoryBase.FindByIdAsync(request.Id);
        company.IsActive = !company.IsActive;
        _repositoryBase.Update(company);
        return Result.Success(company);
    }
}
