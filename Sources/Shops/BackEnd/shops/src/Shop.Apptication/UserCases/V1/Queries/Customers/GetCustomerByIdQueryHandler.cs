using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;
using static Shop.Contract.Services.V1.PaymentHistories.Response;
using static Shop.Domain.Exceptions.CustomersException;

namespace Shop.Apptication.UserCases.V1.Queries.Customers;
public class GetCustomerByIdQueryHandler : IQueryHandler<Query.GetCustomerByIdQuery, Response.CustomerDataEditResponse>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IRepositoryBase<Village, int> _repositoryVillageBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetCustomerByIdQueryHandler(IRepositoryBase<Customer, Guid> repositoryBase, IRepositoryBase<Village, int> repositoryVillageBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
        _repositoryVillageBase = repositoryVillageBase;
    }
    public async Task<Result<Response.CustomerDataEditResponse>> Handle(Query.GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CustomerNotFoundException(request.Id);
        var village = await _repositoryVillageBase.FindSingleAsync(x => x.Id == customer.VillageId);
        var payments = _mapper.Map<List<PaymentHistoryResponse>>(customer.PaymentHistories);
        var result = new Response.CustomerDataEditResponse(customer.Id, customer.Code, customer.Name, customer.Address, customer.Email, customer.PhoneNumber, customer.VillageId, village.WardId, village.Ward.DistrictId, village.Ward.District.CityId, payments);
        return Result.Success(result);
    }
}
