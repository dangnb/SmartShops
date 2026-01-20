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
using Shop.Contract.Services.V1.Provinces;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;

namespace Shop.Apptication.UserCases.V1.Metadata.Queries.Provincies;
public class GetByCompanyQueryHandler : IQueryHandler<Query.GetByCompanyQuery, IList<Response.ProvinceResponse>>
{

    private readonly IRepositoryBase<Province, Guid> _cityRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _userProvider;
    public GetByCompanyQueryHandler(IRepositoryBase<Province, Guid> cityRepository, IMapper mapper, ICurrentUser userProvider)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _userProvider = userProvider;
    }
    public async Task<Result<IList<Response.ProvinceResponse>>> Handle(Query.GetByCompanyQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.FindAll(x => x.ComId == _userProvider.GetRequiredCompanyId()).ToListAsync();
        var result = _mapper.Map<IList<Response.ProvinceResponse>>(cities);
        return Result.Success(result);
    }
}
