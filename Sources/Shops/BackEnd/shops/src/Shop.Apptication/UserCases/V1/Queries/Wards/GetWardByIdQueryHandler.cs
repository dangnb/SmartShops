using AutoMapper;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Wards;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using static Shop.Domain.Exceptions.VillagesException;
using static Shop.Domain.Exceptions.WardsException;

namespace Shop.Apptication.UserCases.V1.Queries.Wards;
public class GetWardByIdQueryHandler : IQueryHandler<Query.GetWardByIdQuery, Response.WardDetailResponse>
{
    private readonly IRepositoryBase<Ward, Guid> _repositoryBase;
    private readonly IMapper _mapper;
    public GetWardByIdQueryHandler(IRepositoryBase<Ward, Guid> repositoryBase, IMapper mapper)
    {
        _repositoryBase = repositoryBase;
        _mapper = mapper;
    }
    public async Task<Result<Response.WardDetailResponse>> Handle(Query.GetWardByIdQuery request, CancellationToken cancellationToken)
    {
        var ward = await _repositoryBase.FindByIdAsync(request.Id) ?? throw new WardNotFoundException(request.Id);
        var result = new Response.WardDetailResponse(ward.Id, ward.ProvincyId, ward.Code, ward.Name, ward.Provincy.Name);
        return Result.Success(result);
    }
}
