using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CitiesException;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Provincies;
public class GetProvinceByIdQueryHandler : IQueryHandler<Query.GetProvinceByIdQuery, Response.ProvinceResponse>
{
    private readonly IRepositoryBase<Province, Guid> _cityRepository;
    private readonly IMapper _mapper;
    public GetProvinceByIdQueryHandler(IRepositoryBase<Province, Guid> cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }
    public async Task<Result<Response.ProvinceResponse>> Handle(Query.GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.FindByIdAsync(request.Id) ?? throw new CityNotFoundException(request.Id);
        var result = _mapper.Map<Response.ProvinceResponse>(city);
        return Result.Success(result);
    }
}
