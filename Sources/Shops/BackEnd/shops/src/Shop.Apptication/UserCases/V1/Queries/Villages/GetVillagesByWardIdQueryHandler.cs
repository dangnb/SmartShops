using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Villages;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;
namespace Shop.Apptication.UserCases.V1.Queries.Villages;
public class GetVillagesByWardIdQueryHandler : IQueryHandler<Query.GetVillagesByWardIdQuery, IList<Response.VillageResponse>>
{
    private readonly IRepositoryBase<Village, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetVillagesByWardIdQueryHandler(IRepositoryBase<Village, int> repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<Response.VillageResponse>>> Handle(Query.GetVillagesByWardIdQuery request, CancellationToken cancellationToken)
    {
        List<Village> villages = await _repositoryBase.FindAll().Where(x => x.ComId == _userProvider.GetComID() && x.WardId == request.WardId).ToListAsync();
        IList<Response.VillageResponse> result = _mapper.Map<List<Response.VillageResponse>>(villages);
        return Result.Success(result);
    }
}
