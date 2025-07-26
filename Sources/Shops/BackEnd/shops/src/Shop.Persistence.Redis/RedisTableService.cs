using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Shop.Persistence.Redis;

public class RedisTableService<T>
{
    private readonly IConnectionMultiplexer _redis;

    public RedisTableService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public void SaveRecordToRedis(List<T> obj, int comId)
    {
        string tableName = nameof(obj);
        Type type = obj.GetType();

        // Lấy tất cả properties
        PropertyInfo[] properties = typeof(T).GetProperties();
        var db = _redis.GetDatabase();
        var hashKey = $"{tableName}:{comId}";

        // Sử dụng Redis Hash để lưu trữ từng bản ghi
        foreach (var property in properties)
        { 
            
        }
        var hashEntries = new HashEntry[]
        {
            new HashEntry("ID", id),
            new HashEntry("Name", name),
            new HashEntry("Age", age),
            new HashEntry("Email", email)
        };

        // Lưu vào Redis
        db.HashSet(hashKey, hashEntries);
    }

    public Dictionary<string, string> GetRecordFromRedis(string tableName, int id)
    {
        var db = _redis.GetDatabase();
        var hashKey = $"{tableName}:{id}";

        // Lấy bản ghi từ Redis Hash
        var entries = db.HashGetAll(hashKey);

        return entries.ToDictionary(entry => entry.Name.ToString(), entry => entry.Value.ToString());
    }
}

