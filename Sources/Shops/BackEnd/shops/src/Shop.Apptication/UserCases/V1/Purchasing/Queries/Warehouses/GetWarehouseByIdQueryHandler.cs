using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Purchasing.V1.Warehouses;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Purchasing.Queries.Warehouses;

public class GetWarehousesByIdQueryHandler(IRepositoryBase<Warehouse, Guid> repositoryBase) : IQueryHandler<Query.GeWarehouseByIdQuery, Response.WarehouseResponse>
{
    private readonly IRepositoryBase<Warehouse, Guid> _repositoryBase = repositoryBase;

    public async Task<Result<Response.WarehouseResponse>> Handle(Query.GeWarehouseByIdQuery request, CancellationToken cancellationToken)
    {
        var ward = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new WardNotFoundException(request.Id);
        var result = new Response.WarehouseResponse(ward.Id, ward.Code, ward.Name, ward.Address, ward.IsActive);
        return Result.Success(result);
    }
}
