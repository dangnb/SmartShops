using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Companies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Companies;
public class UpdateCompanyCommandHandler : ICommandHandler<Command.UpdateCompanyCommand>
{
    private readonly IRepositoryBase<Company, Guid> _repositoryBase;
    public UpdateCompanyCommandHandler(IRepositoryBase<Company, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    public async Task<Result> Handle(Command.UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _repositoryBase.FindByIdAsync(request.Id);
        company.Update(request.Name, request.Addess, request.Phone, request.Mail);
        _repositoryBase.Update(company);
        return Result.Success(company);
    }
}
