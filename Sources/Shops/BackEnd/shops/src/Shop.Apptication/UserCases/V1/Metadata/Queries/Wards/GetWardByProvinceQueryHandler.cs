using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Wards;
public class GetWardByProvinceQueryHandler : IQueryHandler<Query.GetWardsByProvinceQuery, IList<Response.WardResponse>>
{
    private readonly IRepositoryBase<Ward, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    public GetWardByProvinceQueryHandler(IRepositoryBase<Ward, Guid> repositoryBase, IMapper mapper)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
    }
    public async Task<Result<IList<Response.WardResponse>>> Handle(Query.GetWardsByProvinceQuery request, CancellationToken cancellationToken)
    {
        var wards = await _repositoryBase.FindAll().Where(x => x.ProvinceId == request.ProvinceId).ToListAsync();
        var result = _mapper.Map<IList<Response.WardResponse>>(wards);
        return Result.Success(result);
    }
}
