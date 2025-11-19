using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Suppliers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Queries.Suppliers;
public class GetSupplierByIdQueryHandler : IQueryHandler<Query.GetSupplierByIdQuery, Response.SupplierDetailResponse>
{
    private readonly IRepositoryBase<Supplier, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    public GetSupplierByIdQueryHandler(IRepositoryBase<Supplier, Guid> repositoryBase, IMapper mapper)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
    }
    public async Task<Result<Response.SupplierDetailResponse>> Handle(Query.GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new WardNotFoundException(request.Id);
        var result = _mapper.Map<Response.SupplierDetailResponse>(supplier);
        return Result.Success(result);
    }
}
