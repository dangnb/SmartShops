using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.VillagesException;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Queries.Wards;
public class GetWardByIdQueryHandler : IQueryHandler<Query.GetWardByIdQuery, Response.WardDetailResponse>
{
    private readonly IRepositoryBase<Ward, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetWardByIdQueryHandler(IRepositoryBase<Ward, int> repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.WardDetailResponse>> Handle(Query.GetWardByIdQuery request, CancellationToken cancellationToken)
    {
        var ward = await _repositoryBase.FindByIdAsync(request.ID) ?? throw new WardNotFoundException(request.ID);
        var result = new Response.WardDetailResponse(ward.Id, ward.DistrictId, ward.Code, ward.Name, ward.District.Name, ward.District.CityId);
        return Result.Success(result);
    }
}
