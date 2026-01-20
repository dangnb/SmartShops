using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Categories;
public static class Query
{
    public record GetCategoriesQuery(string? SearchTerm, int? ParentId, int PageIndex, int PageSize) : IQuery<List<Response.CategoryTreeResponse>>;
    public record GetCategoryByIdQuery(Guid Id) : IQuery<Response.CategoryResponse>;
}
