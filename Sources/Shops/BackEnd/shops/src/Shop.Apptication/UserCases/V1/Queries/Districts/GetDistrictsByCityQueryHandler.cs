using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Districts;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Districts;
public class GetDistrictsByCityQueryHandler : IQueryHandler<Query.GetDistrictsByCityQuery, IList<Response.DistrictResponse>>
{
    private readonly IRepositoryBase<District, int> _districtRepository;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetDistrictsByCityQueryHandler(IRepositoryBase<District, int> districtRepository, IMapper mapper, IUserProvider userProvider)
    {
        _districtRepository = districtRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<Response.DistrictResponse>>> Handle(Query.GetDistrictsByCityQuery request, CancellationToken cancellationToken)
    {
        var districts = await _districtRepository.FindAll().Where(x => x.ComId == _userProvider.GetComID() && x.CityId == request.cityId).ToListAsync();
        var result = _mapper.Map<IList<Response.DistrictResponse>>(districts);
        return Result.Success(result);
    }
}
