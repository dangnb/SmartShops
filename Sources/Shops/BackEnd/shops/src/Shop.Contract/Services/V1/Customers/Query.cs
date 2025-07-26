using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;

namespace Shop.Contract.Services.V1.Customers;
public static class Query
{
    public record GetCustomersQuery(string? SearchTerm, int? CityId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.CustomerResponse>>;

    /// <summary>
    ///Lấy giữ liệu theo trạng thái thanh toán
    /// </summary>
    /// <param name="SearchTerm"></param>
    /// <param name="Year"></param>
    /// <param name="CityId"></param>
    /// <param name="PageIndex"></param>
    /// <param name="PageSize"></param>
    public record GetCustomersPaidQuery(string? SearchTerm, int Year, int? CityId, int PageIndex, int PageSize) : IQuery<PagedResult<Response.CustomerResponse>>;

    public record GetCustomerByIdQuery(Guid Id) : IQuery<Response.CustomerDataEditResponse>;
}
