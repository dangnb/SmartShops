using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Queries.Customers;
public class GetCustomersQueryHandler : IQueryHandler<Query.GetCustomersQuery, PagedResult<Response.CustomerResponse>>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public GetCustomersQueryHandler(IRepositoryBase<Customer, Guid> wardRepository, IMapper mapper, ICurrentUser currentUser)
    {
        _repositoryBase = wardRepository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<PagedResult<Response.CustomerResponse>>> Handle(Query.GetCustomersQuery request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        var customerQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll(x => x.ComId == comId)
          : _repositoryBase.FindAll(x => x.ComId == comId && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        var customers = await PagedResult<Customer>.CreateAsync(customerQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.CustomerResponse>>(customers);
        return Result.Success(result);
    }
}
