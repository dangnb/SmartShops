using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Villages;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Villages;
public class GetVillagesQueryHandler : IQueryHandler<Query.GetVillagesQuery, PagedResult<Response.VillageResponse>>
{
    private readonly IRepositoryBase<Village, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;

    public GetVillagesQueryHandler(IRepositoryBase<Village, int> districtRepository, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = districtRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.VillageResponse>>> Handle(Query.GetVillagesQuery request, CancellationToken cancellationToken)
    {
        var villageQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetComID())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetComID() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        if (request.WardId.HasValue && request.WardId > 0)
        {
            villageQuery = villageQuery.Where(x => x.WardId == request.WardId);
        }
        var products = await PagedResult<Village>.CreateAsync(villageQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.VillageResponse>>(products);
        return Result.Success(result);
    }
}
