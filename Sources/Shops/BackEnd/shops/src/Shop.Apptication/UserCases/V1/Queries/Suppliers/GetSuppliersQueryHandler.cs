using AutoMapper;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Suppliers;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace TP.Apptication.UserCases.V1.Queries.Suppliers;
public class GetSuppliersQueryHandler : IQueryHandler<Query.GetSuppliersQuery, PagedResult<Response.SupplierResponse>>
{
    private readonly IRepositoryBase<Supplier, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;

    public GetSuppliersQueryHandler(IRepositoryBase<Supplier, Guid> wardRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _repositoryBase = wardRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.SupplierResponse>>> Handle(Query.GetSuppliersQuery request, CancellationToken cancellationToken)
    {
        var wardQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetRequiredCompanyId())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        if (request.ProvinceId.HasValue)
        {
            wardQuery = wardQuery.Where(x => x.ProvinceId == request.ProvinceId);
        }
        var products = await PagedResult<Supplier>.CreateAsync(wardQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.SupplierResponse>>(products);
        return Result.Success(result);
    }
}
