using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Districts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Districts;
public class GetVillagesQueryHandler : IQueryHandler<Query.GetDistrictsQuery, PagedResult<Response.DistrictResponse>>
{
    private readonly IRepositoryBase<District, int> _districtRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;

    public GetVillagesQueryHandler(IRepositoryBase<District, int> districtRepository, IMapper mapper, IUserProvider userProvider)
    {
        _districtRepository = districtRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }

    public async Task<Result<PagedResult<Response.DistrictResponse>>> Handle(Query.GetDistrictsQuery request, CancellationToken cancellationToken)
    {
        var districtQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _districtRepository.FindAll().Where(x => x.ComId == _userProvider.GetComID())
          : _districtRepository.FindAll(x => x.ComId == _userProvider.GetComID() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));
        if (request.CityId.HasValue)
        {
            districtQuery = districtQuery.Where(x => x.CityId == request.CityId);
        }
        var products = await PagedResult<District>.CreateAsync(districtQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.DistrictResponse>>(products);
        return Result.Success(result);
    }
}
