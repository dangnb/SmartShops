using AutoMapper;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.V1.Common.Payments.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Identity;
using Shop.Domain.Entities.Purchases;
using static Shop.Contract.Services.V1.Purchasing.Warehouses.Response;
using static Shop.Contract.Services.V1.Common.Categories.Response;
using static Shop.Contract.Services.V1.Common.Customers.Response;
using static Shop.Contract.Services.V1.Common.Payments.Response;
using static Shop.Contract.Services.V1.Common.Permissions.Response;
using static Shop.Contract.Services.V1.Common.Products.Response;
using static Shop.Contract.Services.V1.Common.Provincies.Response;
using static Shop.Contract.Services.V1.Common.Roles.Response;
using static Shop.Contract.Services.V1.Common.Suppliers.Response;
using static Shop.Contract.Services.V1.Common.Users.Response;
using static Shop.Contract.Services.V1.Common.Wards.Response;

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

        //Categories
        CreateMap<Category, CategoryResponse>().ReverseMap();
        CreateMap<Category, CategoryTreeResponse>().ReverseMap();
        CreateMap<PagedResult<Category>, PagedResult<CategoryResponse>>().ReverseMap();


        //Products
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<PagedResult<Product>, PagedResult<ProductResponse>>().ReverseMap();

        //Products
        CreateMap<Warehouse, WarehouseResponse>().ReverseMap();
        CreateMap<PagedResult<Warehouse>, PagedResult<WarehouseResponse>>().ReverseMap();

    }
}
