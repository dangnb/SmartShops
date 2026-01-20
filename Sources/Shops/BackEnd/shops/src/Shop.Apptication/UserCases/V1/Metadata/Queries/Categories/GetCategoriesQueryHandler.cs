using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Categories;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Categories;

public class GetCategoriesQueryHandler : IQueryHandler<Query.GetCategoriesQuery, List<Response.CategoryTreeResponse>>
{
    private readonly IRepositoryBase<Category, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public GetCategoriesQueryHandler(IRepositoryBase<Category, Guid> repositoryBase, IMapper mapper, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<List<Response.CategoryTreeResponse>>> Handle(Query.GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        Guid comId = _currentUser.GetRequiredCompanyId();
        var customerQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll(x => x.ComId == comId)
          : _repositoryBase.FindAll(x => x.ComId == comId && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        var lst = _mapper.Map<List<Response.CategoryTreeResponse>>(await customerQuery.ToListAsync());
        var result = CategoryTreeBuilder.BuildTree(lst);
        return Result.Success(result);
    }
}
