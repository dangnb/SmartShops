using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Metadata;

namespace Shop.Apptication.UserCases.V1.Queries.Wards;
public class GetWardByDistrictQueryHandler : IQueryHandler<Query.GetWardByDistrictQuery, IList<Response.WardResponse>>
{
    private readonly IRepositoryBase<Ward, int> _repositoryBase;
    private readonly IMapper _mapper;
    private readonly IUserProvider _userProvider;
    public GetWardByDistrictQueryHandler(IRepositoryBase<Ward, int> repositoryBase, IMapper mapper, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<Response.WardResponse>>> Handle(Query.GetWardByDistrictQuery request, CancellationToken cancellationToken)
    {
        var wards = await _repositoryBase.FindAll().Where(x => x.DistrictId == request.DistrictId).ToListAsync();
        var result = _mapper.Map<IList<Response.WardResponse>>(wards);
        return Result.Success(result);
    }
}
