namespace Shop.Contract.Extensions;

public class UserContext
{
    public string Username { get; set; }
    public string TaxCode { get; set; }

    public UserContext(string username, string taxcode)
    {
        TaxCode = taxcode;
        Username = username;
    }
}
