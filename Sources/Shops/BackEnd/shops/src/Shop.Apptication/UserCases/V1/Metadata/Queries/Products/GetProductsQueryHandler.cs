using AutoMapper;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Products;

public class GetProductsQueryHandler : IQueryHandler<Query.GetProductsQuery, PagedResult<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Product, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;

    public GetProductsQueryHandler(IRepositoryBase<Product, Guid> prodRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _repositoryBase = prodRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.ProductResponse>>> Handle(Query.GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetRequiredCompanyId())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        if (request.CategoryId.HasValue)
        {
            productQuery = productQuery.Where(x => x.CategoryId == request.CategoryId);
        }
        PagedResult<Product> products = await PagedResult<Product>.CreateAsync(productQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.ProductResponse>>(products);
        return Result.Success(result);
    }
}
