namespace Shop.Domain.Dappers.Repositories;
public interface IGenericRepository<T>
    where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string tableName);
    Task<T> GetByIdAsync(string tableName, object id);
    Task<IEnumerable<E>> GetQueryAsync<E>(string query, Common.SQLParam[] parameters = null);
    Task<(IEnumerable<T> data, int totalCount)> GetPagedAsync(string query, int pageNumber, int pageSize, Common.SQLParam[] parameters = null);
    Task<(IEnumerable<E> data, int totalCount)> GetDynamicPagedAsync<E>(string query, int pageNumber, int pageSize, Common.SQLParam[] parameters = null);
}
