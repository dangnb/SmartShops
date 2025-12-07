
using Microsoft.AspNetCore.Http;
using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Categories;

public static class Command
{
    public record CreateCategoryCommand(string Code, string Name, Guid? ParentId, int? SortOrder, int? Level, bool IsActive) : ICommand;
    public record UpdateCategoryCommand(Guid Id, string Code, string Name, Guid? ParentId, int? SortOrder, int? Level, bool IsActive) : ICommand;
    public record DeleteCategoryCommand(Guid Id) : ICommand;
    public record UploadCategoryCommand(IFormFile File) : ICommand;
}
