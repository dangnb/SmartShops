using AutoMapper;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Payments.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;
using static Shop.Contract.Services.V1.Customers.Response;
using static Shop.Contract.Services.V1.Payments.Response;
using static Shop.Contract.Services.V1.Permissions.Response;
using static Shop.Contract.Services.V1.Products.Response;
using static Shop.Contract.Services.V1.Provinces.Response;
using static Shop.Contract.Services.V1.Roles.Response;
using static Shop.Contract.Services.V1.Suppliers.Response;
using static Shop.Contract.Services.V1.Users.Response;
using static Shop.Contract.Services.V1.Wards.Response;

namespace Shop.Application.Mapper;

internal class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Permission, PermissionResponse>().ReverseMap();
        CreateMap<PagedResult<Permission>, PagedResult<PermissionResponse>>().ReverseMap();

        //Users
        CreateMap<AppUser, UserInforByToken>().ReverseMap();


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


        // Payments
        CreateMap<Payment, PaymentResponse>();
        CreateMap<PaymentDto, PaymentResponse>();
        CreateMap<PagedResult<PaymentDto>, PagedResult<PaymentResponse>>().ReverseMap();


        //Customers
        CreateMap<AppUser, UserResponse>().ReverseMap();
        CreateMap<PagedResult<AppUser>, PagedResult<UserResponse>>().ReverseMap();


        CreateMap<Province, ProvinceResponse>().ReverseMap();
        CreateMap<PagedResult<Province>, PagedResult<ProvinceResponse>>().ReverseMap();
        CreateMap<Ward, WardResponse>().ForMember(
                  dest => dest.ProvinceName,
                  opt => opt.MapFrom(src => src.Province.Name)).ReverseMap();
        CreateMap<PagedResult<Ward>, PagedResult<WardResponse>>().ReverseMap();


        //Customers
        CreateMap<Supplier, SupplierResponse>().ReverseMap();
        CreateMap<Supplier, SupplierDetailResponse>().ReverseMap();
        CreateMap<PagedResult<Supplier>, PagedResult<SupplierResponse>>().ReverseMap();

    }
}
