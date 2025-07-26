using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Customers;
public class GetCustomersQueryHandler : IQueryHandler<Query.GetCustomersQuery, PagedResult<Response.CustomerResponse>>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;

    public GetCustomersQueryHandler(IRepositoryBase<Customer, Guid> wardRepository, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = wardRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.CustomerResponse>>> Handle(Query.GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetComID())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetComID() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        var customers = await PagedResult<Customer>.CreateAsync(customerQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.CustomerResponse>>(customers);
        return Result.Success(result);
    }
}
