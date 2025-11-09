namespace Shop.Apptication.Exceptions;
public interface IUserProvider
{
    string GetUserName();
    Guid GetComID();
    string GetTaxCode();
}
