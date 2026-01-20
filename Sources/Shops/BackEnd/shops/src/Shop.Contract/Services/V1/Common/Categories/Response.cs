namespace Shop.Contract.Services.V1.Common.Categories;

public class Response
{
    public record CategoryResponse(Guid Id, string Code, string Name, Guid? ParentId, int? SortOrder, int? Level, bool IsActive);

    public record CategoryTreeResponse(
    Guid Id,
    string Code,
    string Name,
    Guid? ParentId,
    int? SortOrder,
    List<CategoryTreeResponse> Children);
}
