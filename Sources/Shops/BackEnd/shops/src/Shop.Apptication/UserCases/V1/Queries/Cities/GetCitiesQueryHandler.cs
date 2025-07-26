using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Cities;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Cities;
public class GetCitiesQueryHandler : IQueryHandler<Query.GetCitiesQuery, PagedResult<Response.CityResponse>>
{
    private readonly IRepositoryBase<City, int> _cityRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetCitiesQueryHandler(IRepositoryBase<City, int> cityRepository, IMapper mapper, IUserProvider userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<PagedResult<Response.CityResponse>>> Handle(Query.GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _cityRepository.FindAll().Where(x => x.ComId == _userProvider.GetComID())
          : _cityRepository.FindAll(x => x.ComId == _userProvider.GetComID() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));

        var products = await PagedResult<City>.CreateAsync(productsQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.CityResponse>>(products);
        return Result.Success(result);
    }
}
