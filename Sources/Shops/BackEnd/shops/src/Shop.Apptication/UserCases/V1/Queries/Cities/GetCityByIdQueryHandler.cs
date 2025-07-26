using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Cities;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.CitiesException;

namespace Shop.Apptication.UserCases.V1.Queries.Cities;
public class GetDistrictByIdQueryHandler : IQueryHandler<Query.GetCityByIdQuery, Response.CityResponse>
{
    private readonly IRepositoryBase<City, int> _cityRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetDistrictByIdQueryHandler(IRepositoryBase<City, int> cityRepository, IMapper mapper, IUserProvider userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.CityResponse>> Handle(Query.GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.FindByIdAsync(request.ID) ?? throw new CityNotFoundException(request.ID);
        var result = _mapper.Map<Response.CityResponse>(city);
        return Result.Success(result);
    }
}
