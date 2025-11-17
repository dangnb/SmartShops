using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.CitiesException;

namespace Shop.Apptication.UserCases.V1.Queries.Provincies;
public class GetProvincyByIdQueryHandler : IQueryHandler<Query.GetProvincyByIdQuery, Response.ProvincyResponse>
{
    private readonly IRepositoryBase<Provincy, Guid> _cityRepository;
    private readonly IMapper _mapper;
    public GetProvincyByIdQueryHandler(IRepositoryBase<Provincy, Guid> cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }
    public async Task<Result<Response.ProvincyResponse>> Handle(Query.GetProvincyByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.FindByIdAsync(request.Id) ?? throw new CityNotFoundException(request.Id);
        var result = _mapper.Map<Response.ProvincyResponse>(city);
        return Result.Success(result);
    }
}
