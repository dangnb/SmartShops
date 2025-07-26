using AutoMapper;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Villages;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
using static Shop.Domain.Exceptions.VillagesException;

namespace Shop.Apptication.UserCases.V1.Queries.Villages;
public class GetVillageByIdQueryHandler : IQueryHandler<Query.GetVillageByIdQuery, Response.VillageDetailResponse>
{
    private readonly IRepositoryBase<Village, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetVillageByIdQueryHandler(IRepositoryBase<Village, int> repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<Response.VillageDetailResponse>> Handle(Query.GetVillageByIdQuery request, CancellationToken cancellationToken)
    {
        var village = await _repositoryBase.FindByIdAsync(request.ID) ?? throw new VillageNotFoundException(request.ID);
        var result = new Response.VillageDetailResponse(village.Id, village.WardId, village.Code, village.Name, village.UserName, village.Ward.Name, village.Ward.DistrictId, village.Ward.District.CityId);
        return Result.Success(result);
    }
}
