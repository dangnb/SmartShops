using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Categories;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CategoriesException;

namespace Shop.Apptication.UserCases.V1.Queries.Categories;

public class GetCategoryByIdQueryHandler : IQueryHandler<Query.GetCategoryByIdQuery, Response.CategoryResponse>
{
    private readonly IRepositoryBase<Category, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    public GetCategoryByIdQueryHandler(IRepositoryBase<Category, Guid> repositoryBase, IMapper mapper)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
    }
    public async Task<Result<Response.CategoryResponse>> Handle(Query.GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new CategoryNotFoundException();
        var result = _mapper.Map<Response.CategoryResponse>(category);
        return Result.Success(result);
    }
}
