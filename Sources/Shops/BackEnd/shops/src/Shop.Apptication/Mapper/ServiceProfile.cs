using AutoMapper;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Payments.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;
using Shop.Domain.Entities.Metadata;
using static Shop.Contract.Services.V1.Cities.Response;
using static Shop.Contract.Services.V1.Customers.Response;
using static Shop.Contract.Services.V1.Districts.Response;
using static Shop.Contract.Services.V1.PaymentHistories.Response;
using static Shop.Contract.Services.V1.Payments.Response;
using static Shop.Contract.Services.V1.Permissions.Response;
using static Shop.Contract.Services.V1.Products.Response;
using static Shop.Contract.Services.V1.Roles.Response;
using static Shop.Contract.Services.V1.Users.Response;
using static Shop.Contract.Services.V1.Villages.Response;
using static Shop.Contract.Services.V1.Wards.Response;

namespace Shop.Application.Mapper;
internal class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        //CreateMap<Product, Response.ProductResponse>().ReverseMap();
        //CreateMap<PagedResult<Product>, PagedResult<Response.ProductResponse>>().ReverseMap();


        CreateMap<Permission, PermissionResponse>().ReverseMap();
        CreateMap<PagedResult<Permission>, PagedResult<PermissionResponse>>().ReverseMap();

        //Users
        CreateMap<AppUser, UserInforByToken>().ReverseMap();


        //Cities
        CreateMap<City, CityResponse>().ReverseMap();
        CreateMap<PagedResult<City>, PagedResult<CityResponse>>().ReverseMap();

        //Districts
        CreateMap<District, DistrictResponse>().ForMember(
                  dest => dest.CityName,
                  opt => opt.MapFrom(src => src.City.Name)).ReverseMap();
        CreateMap<PagedResult<District>, PagedResult<DistrictResponse>>().ReverseMap();

        //Wards
        CreateMap<Ward, WardResponse>().ForMember(
                  dest => dest.DistrictName,
                  opt => opt.MapFrom(src => src.District.Name))
            .ReverseMap();
        CreateMap<PagedResult<Ward>, PagedResult<WardResponse>>().ReverseMap();

        //Wards
        CreateMap<Village, VillageResponse>().ForMember(
                  dest => dest.WardName,
                  opt => opt.MapFrom(src => src.Ward.Name)).ReverseMap();
        CreateMap<PagedResult<Village>, PagedResult<VillageResponse>>().ReverseMap();

        //Roles
        CreateMap<AppRole, RoleResponse>().ReverseMap();
        CreateMap<PagedResult<AppRole>, PagedResult<RoleResponse>>().ReverseMap();

        //Customers
        CreateMap<Customer, CustomerResponse>()
            .ReverseMap();
        CreateMap<PagedResult<Customer>, PagedResult<CustomerResponse>>().ReverseMap();

        //Customers
        CreateMap<Product, ProductResponse>()
            .ReverseMap();
        CreateMap<PagedResult<Product>, PagedResult<ProductResponse>>().ReverseMap();

        // PaymentHistories
        CreateMap<PaymentHistory, PaymentHistoryResponse>();

        // Payments
        CreateMap<Payment, PaymentResponse>();
        CreateMap<PaymentDto, PaymentResponse>();
        CreateMap<PagedResult<PaymentDto>, PagedResult<PaymentResponse>>().ReverseMap();


        //Customers
        CreateMap<AppUser, UserResponse>().ReverseMap();
        CreateMap<PagedResult<AppUser>, PagedResult<UserResponse>>().ReverseMap();




    }
}
