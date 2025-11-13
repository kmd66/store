namespace MizeBazi.Store.Common.Abstractions;
public interface ICacheService
{
    T Get<T>(string group, Guid key);
    void Set<T>(string group, Guid key, T value, int slidingExpiry = 0, int absoluteExpiry = 0);
    bool TryGetValue<T>(string group, Guid key, out T value);
    bool TryGetValue<T>(string group, string key, out T value);
    void Remove(string key);
    void Remove(string group, Guid key);
    bool Exists(string group, Guid key);
    IEnumerable<KeyValuePair<string, T>> SearchByPartition<T>(string partition, Func<T, bool> valueFilter = null);
}