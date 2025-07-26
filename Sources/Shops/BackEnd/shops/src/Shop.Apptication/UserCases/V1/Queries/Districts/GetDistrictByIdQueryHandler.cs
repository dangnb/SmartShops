using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Districts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.CitiesException;

namespace Shop.Apptication.UserCases.V1.Queries.Districts;
public class GetDistrictByIdQueryHandler : IQueryHandler<Query.GetDistrictByIdQuery, Response.DistrictResponse>
{
    private readonly IRepositoryBase<District, int> _cityRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetDistrictByIdQueryHandler(IRepositoryBase<District, int> cityRepository, IMapper mapper, IUserProvider userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.DistrictResponse>> Handle(Query.GetDistrictByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.FindByIdAsync(request.ID) ?? throw new CityNotFoundException(request.ID);
        var result = _mapper.Map<Response.DistrictResponse>(city);
        return Result.Success(result);
    }
}
