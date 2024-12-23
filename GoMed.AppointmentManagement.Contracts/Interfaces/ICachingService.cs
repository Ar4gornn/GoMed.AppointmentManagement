namespace GoMed.AppointmentManagement.Contracts.Interfaces;

public interface ICachingService
{
    Task<bool> SetValueAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<T?> GetValueAsync<T>(string key);
    Task<bool> RemoveValueAsync(string key);
    Task<bool> KeyExistsAsync(string key);
    Task<bool> KeyDeleteAsync(string key);
}