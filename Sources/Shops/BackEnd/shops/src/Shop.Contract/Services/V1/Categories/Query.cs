using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Categories;
public static class Query
{
    public record GetCustomersQuery(string? SearchTerm, int? CityId, int PageIndex, int PageSize) : IQuery<List<Response.CategoryTreeResponse>>;


    public record GetCustomerByIdQuery(Guid Id) : IQuery<Response.CategoryResponse>;
}
