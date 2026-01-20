using AutoMapper;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Purchasing.Warehouses;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.V1.Purchasing.Queries.Warehouses;
public class GetWarehousesQueryHandler : IQueryHandler<Query.GetWarehousesQuery, PagedResult<Response.WarehouseResponse>>
{
    private readonly IRepositoryBase<Warehouse, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;

    public GetWarehousesQueryHandler(IRepositoryBase<Warehouse, Guid> wardRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _repositoryBase = wardRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.WarehouseResponse>>> Handle(Query.GetWarehousesQuery request, CancellationToken cancellationToken)
    {
        var wardQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetRequiredCompanyId())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        var products = await PagedResult<Warehouse>.CreateAsync(wardQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.WarehouseResponse>>(products);
        return Result.Success(result);
    }
}
