using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Customers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CustomersException;

namespace Shop.Apptication.UserCases.V1.Queries.Customers;
public class GetCustomerByIdQueryHandler : IQueryHandler<Query.GetCustomerByIdQuery, Response.CustomerResponse>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    public GetCustomerByIdQueryHandler(IRepositoryBase<Customer, Guid> repositoryBase,IMapper mapper)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
    }
    public async Task<Result<Response.CustomerResponse>> Handle(Query.GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CustomerNotFoundException(request.Id);
        var result = new Response.CustomerResponse(customer.Id, customer.Code, customer.Name, customer.Address, customer.Email, customer.PhoneNumber, customer.CitizenIdNumber, customer.PassportNumber);
        return Result.Success(result);
    }
}
