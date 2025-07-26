using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Wards;
public class GetWardsQueryHandler : IQueryHandler<Query.GetWardsQuery, PagedResult<Response.WardResponse>>
{
    private readonly IRepositoryBase<Ward, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;

    public GetWardsQueryHandler(IRepositoryBase<Ward, int> wardRepository, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = wardRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.WardResponse>>> Handle(Query.GetWardsQuery request, CancellationToken cancellationToken)
    {
        var villageQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetComID())
          : _repositoryBase.FindAll(x => x.ComId == _userProvider.GetComID() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        if (request.DistrictId.HasValue && request.DistrictId > 0)
        {
            villageQuery = villageQuery.Where(x => x.DistrictId == request.DistrictId);
        }
        var products = await PagedResult<Ward>.CreateAsync(villageQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.WardResponse>>(products);
        return Result.Success(result);
    }
}
