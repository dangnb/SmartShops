using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shop.Contract.Services.V1.Categories.Response;

namespace Shop.Apptication.UserCases.V1.Queries.Categories;

public static class CategoryTreeBuilder
{
    public static List<CategoryTreeResponse> BuildTree(IEnumerable<CategoryTreeResponse> flatCategories)
    {
        var lookup = flatCategories.ToDictionary(c => c.Id);
        var roots = new List<CategoryTreeResponse>();

        foreach (var category in lookup.Values)
        {
            if (category.ParentId == null)
            {
                roots.Add(category);
            }
            else if (lookup.TryGetValue(category.ParentId.Value, out var parent))
            {
                parent.Children.Add(category);
            }
        }

        roots = SortList(roots);

        foreach (var root in roots)
        {
            SortChildrenRecursively(root);
        }

        return roots;
    }

    private static void SortChildrenRecursively(CategoryTreeResponse node)
    {
        // Instead of assigning to the init-only property, sort the list in place
        node.Children.Sort((a, b) =>
        {
            int orderCompare = Nullable.Compare(a.SortOrder, b.SortOrder);
            if (orderCompare != 0) return orderCompare;
            return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
        });

        foreach (CategoryTreeResponse child in node.Children)
        {
            SortChildrenRecursively(child);
        }
    }

    private static List<CategoryTreeResponse> SortList(List<CategoryTreeResponse> list)
    {
        return [.. list
            .OrderBy(c => c.SortOrder ?? int.MaxValue)
            .ThenBy(c => c.Name)];
    }
}
