using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Products;
public class GetProductsQueryHandler : IQueryHandler<Query.GetProductsQuery, PagedResult<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Product, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;

    public GetProductsQueryHandler(IRepositoryBase<Product, int> wardRepository, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = wardRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.ProductResponse>>> Handle(Query.GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetComID())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetComID() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
      
        var products = await PagedResult<Product>.CreateAsync(productQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.ProductResponse>>(products);
        return Result.Success(result);
    }
}
