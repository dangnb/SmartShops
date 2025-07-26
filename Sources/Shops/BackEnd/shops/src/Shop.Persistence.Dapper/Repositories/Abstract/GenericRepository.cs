using System.Data;
using Dapper;
using Shop.Domain.Dappers.Repositories;

namespace Shop.Persistence.Dapper.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IDbConnection _connection;

    public GenericRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<IEnumerable<T>> GetAllAsync(string tableName)
    {
        var sql = $"SELECT * FROM {tableName}";
        return await _connection.QueryAsync<T>(sql);
    }

    public async Task<T> GetByIdAsync(string tableName, object id)
    {
        var sql = $"SELECT * FROM {tableName} WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<(IEnumerable<E> data, int totalCount)> GetDynamicPagedAsync<E>(string query, int pageNumber, int pageSize, Domain.Common.SQLParam[] parameters = null)
    {
        var offset = (pageNumber - 1) * pageSize;
        var sql = $@"{query} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; SELECT COUNT(*) FROM ({query}) as table42";

        var queryParameters = new DynamicParameters();
        foreach (var item in parameters)
        {
            queryParameters.Add(item.key, item.value);
        }
        queryParameters.Add("Offset", offset);
        queryParameters.Add("PageSize", pageSize);

        using (var multi = await _connection.QueryMultipleAsync(sql, queryParameters))
        {
            var data = await multi.ReadAsync<E>();
            var totalCount = await multi.ReadFirstAsync<int>();
            return (data, totalCount);
        }
    }

    public async Task<(IEnumerable<T> data, int totalCount)> GetPagedAsync(string query, int pageNumber, int pageSize, Domain.Common.SQLParam[] parameters = null)
    {
        var offset = (pageNumber - 1) * pageSize;
        var sql = $@"{query} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; SELECT COUNT(*) FROM ({query}) as table42";

        var queryParameters = new DynamicParameters();
        foreach (var item in parameters)
        {
            queryParameters.Add(item.key, item.value);
        }
        queryParameters.Add("Offset", offset);
        queryParameters.Add("PageSize", pageSize);

        using (var multi = await _connection.QueryMultipleAsync(sql, queryParameters))
        {
            var data = await multi.ReadAsync<T>();
            var totalCount = await multi.ReadFirstAsync<int>();
            return (data, totalCount);
        }
    }

    public async Task<IEnumerable<E>> GetQueryAsync<E>(string query, Domain.Common.SQLParam[] parameters = null)
    {
        var queryParameters = new DynamicParameters();
        foreach (var item in parameters)
        {
            queryParameters.Add(item.key, item.value);
        }
        return await _connection.QueryAsync<E>(query, queryParameters);
    }
}
