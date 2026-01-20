using AutoMapper;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Provincies;
public class GetProvincesQueryHandler : IQueryHandler<Query.GetProvincesQuery, PagedResult<Response.ProvinceResponse>>
{
    private readonly IRepositoryBase<Province, Guid> _cityRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;
    public GetProvincesQueryHandler(IRepositoryBase<Province, Guid> cityRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<PagedResult<Response.ProvinceResponse>>> Handle(Query.GetProvincesQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _cityRepository.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId())
          : _cityRepository.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));

        var products = await PagedResult<Province>.CreateAsync(productsQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.ProvinceResponse>>(products);
        return Result.Success(result);
    }
}
