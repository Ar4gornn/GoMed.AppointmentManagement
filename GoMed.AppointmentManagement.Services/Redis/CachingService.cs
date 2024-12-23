using System.Text.Json;
using GoMed.AppointmentManagement.Contracts.Interfaces;
using StackExchange.Redis;

namespace GoMed.AppointmentManagement.Services.Redis;

public class CachingService : ICachingService
{
    private readonly IDatabase _database;

    public CachingService(IConnectionMultiplexer redisConnection)
    {
        _database = redisConnection.GetDatabase();
    }

    public async Task<bool> SetValueAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));

        string serializedValue = JsonSerializer.Serialize(value);
        return await _database.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetValueAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));

        RedisValue redisValue = await _database.StringGetAsync(key);

        if (!redisValue.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(redisValue!);
    }

    public async Task<bool> RemoveValueAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));

        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> KeyExistsAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));

        return await _database.KeyExistsAsync(key);
    }

    public async Task<bool> KeyDeleteAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));

        return await _database.KeyDeleteAsync(key);
    }
}