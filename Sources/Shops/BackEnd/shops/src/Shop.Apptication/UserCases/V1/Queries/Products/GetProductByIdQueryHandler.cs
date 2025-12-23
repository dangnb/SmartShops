using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Products;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Queries.Products;

public class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductByIdQuery, Response.ProductResponse>
{
    private readonly IRepositoryBase<Product, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    public GetProductByIdQueryHandler(IRepositoryBase<Product, Guid> repositoryBase, IMapper mapper)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
    }
    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new WardNotFoundException(request.Id);
        var result = _mapper.Map<Response.ProductResponse>(supplier);
        return Result.Success(result);
    }
}
