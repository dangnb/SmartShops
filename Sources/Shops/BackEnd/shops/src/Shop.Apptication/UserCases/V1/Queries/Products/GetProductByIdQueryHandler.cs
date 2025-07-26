using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.ProductsException;

namespace Shop.Apptication.UserCases.V1.Queries.Products;
public class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductByIdQuery, Response.ProductResponse>
{
    private readonly IRepositoryBase<Product, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetProductByIdQueryHandler(IRepositoryBase<Product, int> repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _repositoryBase.FindByIdAsync(request.ID) ?? throw new ProductNotFoundException(request.ID);
        var result = _mapper.Map<Response.ProductResponse>(response);
        return Result.Success(result);
    }
}
