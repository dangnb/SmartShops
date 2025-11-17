using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Provincies;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Queries.Provincies;
public class GetByCompanyQueryHandler : IQueryHandler<Query.GetByCompanyQuery, IList<Response.ProvincyResponse>>
{

    private readonly IRepositoryBase<Provincy, Guid> _cityRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;
    public GetByCompanyQueryHandler(IRepositoryBase<Provincy, Guid> cityRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<Response.ProvincyResponse>>> Handle(Query.GetByCompanyQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId()).ToListAsync();
        var result = _mapper.Map<IList<Response.ProvincyResponse>>(cities);
        return Result.Success(result);
    }
}
