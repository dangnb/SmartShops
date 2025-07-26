namespace Shop.Contract.Extensions.Products;
public static class ProductExtension
{
    public static string GetSortProductProperty(string sortColumn)
        => sortColumn.ToLower() switch
        {
            "name" => "Name",
            "price" => "Price",
            "description" => "Description",
            _ => "Id"
        };
}
