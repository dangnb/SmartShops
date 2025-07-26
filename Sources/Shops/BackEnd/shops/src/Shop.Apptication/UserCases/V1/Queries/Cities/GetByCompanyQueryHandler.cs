using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Cities;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Cities;
public class GetByCompanyQueryHandler : IQueryHandler<Query.GetByCompanyQuery, IList<Response.CityResponse>>
{

    private readonly IRepositoryBase<City, int> _cityRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetByCompanyQueryHandler(IRepositoryBase<City, int> cityRepository, IMapper mapper, IUserProvider userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<Response.CityResponse>>> Handle(Query.GetByCompanyQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.FindAll().Where(x => x.ComId == _userProvider.GetComID()).ToListAsync();
        var result = _mapper.Map<IList<Response.CityResponse>>(cities);
        return Result.Success(result);
    }
}
