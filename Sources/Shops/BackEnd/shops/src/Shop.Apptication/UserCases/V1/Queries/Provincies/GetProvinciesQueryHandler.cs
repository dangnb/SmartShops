using AutoMapper;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Queries.Provincies;
public class GetProvinciesQueryHandler : IQueryHandler<Query.GetProvinciesQuery, PagedResult<Response.ProvincyResponse>>
{
    private readonly IRepositoryBase<Provincy, Guid> _cityRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;
    public GetProvinciesQueryHandler(IRepositoryBase<Provincy, Guid> cityRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<PagedResult<Response.ProvincyResponse>>> Handle(Query.GetProvinciesQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
          ? _cityRepository.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId())
          : _cityRepository.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId() && (x.Name.Contains(request.SearchTerm) || x.Code.Contains(request.SearchTerm)));

        var products = await PagedResult<Provincy>.CreateAsync(productsQuery,
            request.PageIndex,
            request.PageSize);

        var result = _mapper.Map<PagedResult<Response.ProvincyResponse>>(products);
        return Result.Success(result);
    }
}
