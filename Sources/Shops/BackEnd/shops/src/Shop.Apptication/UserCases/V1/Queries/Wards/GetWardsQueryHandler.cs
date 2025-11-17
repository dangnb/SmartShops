using AutoMapper;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace TP.Apptication.UserCases.V1.Queries.Wards;
public class GetWardsQueryHandler : IQueryHandler<Query.GetWardsQuery, PagedResult<Response.WardResponse>>
{
    private readonly IRepositoryBase<Ward, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;

    public GetWardsQueryHandler(IRepositoryBase<Ward, Guid> wardRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _repositoryBase = wardRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.WardResponse>>> Handle(Query.GetWardsQuery request, CancellationToken cancellationToken)
    {
        var villageQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetRequiredCompanyId())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        if (request.ProvincyId.HasValue)
        {
            villageQuery = villageQuery.Where(x => x.ProvincyId == request.ProvincyId);
        }
        var products = await PagedResult<Ward>.CreateAsync(villageQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.WardResponse>>(products);
        return Result.Success(result);
    }
}
