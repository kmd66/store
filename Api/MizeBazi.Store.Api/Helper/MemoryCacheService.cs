using Microsoft.Extensions.Caching.Memory;
using MizeBazi.Store.Common.Abstractions;
using System.Collections.Concurrent;

namespace MizeBazi.Store.Api.Helper;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly ConcurrentDictionary<string, object> _keys = new();

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T Get<T>(string group, Guid key) => _cache.Get<T>($"{group}_{key}");

    public void Set<T>(string group, Guid key, T value, int slidingExpiry = 0, int absoluteExpiry = 0)
    {
        var k = $"{group}_{key}";
        var options = new MemoryCacheEntryOptions();
        if (slidingExpiry > 0)
        {
            options.SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpiry)); // تمدید با هر خواندن
        }

        if (absoluteExpiry > 0)
        {
            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpiry)); // حداکثر زمان ماندگاری
        }

        options.RegisterPostEvictionCallback(RemoveKeyCallback);

        _cache.Set(k, value, options);
        _keys.TryAdd(k, null);
    }

    public bool TryGetValue<T>(string group, Guid key, out T value)=> _cache.TryGetValue($"{group}_{key}", out value);
    public bool TryGetValue<T>(string group, string key, out T value)=> _cache.TryGetValue($"{group}_{key}", out value);

    public void Remove(string k)
    {
        _cache.Remove(k);
        _keys.TryRemove(k, out _);
    }
    public void Remove(string group, Guid key) => Remove($"{group}_{key}");

    public bool Exists(string group, Guid key) => _cache.TryGetValue($"{group}_{key}", out _);

    private void RemoveKeyCallback(object key, object value, EvictionReason reason, object state)
    {
        _keys.TryRemove(key.ToString(), out _);
    }

    private IEnumerable<KeyValuePair<string, T>> Search<T>(Func<KeyValuePair<string, T>, bool> predicate)
    {
        foreach (var key in _keys.Keys)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                var item = new KeyValuePair<string, T>(key, value);
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
    public IEnumerable<KeyValuePair<string, T>> SearchByPartition<T>(string partition, Func<T, bool> valueFilter = null)
    {
        var items = Search<T>(item => item.Key.StartsWith($"{partition}_"));

        if (valueFilter != null)
        {
            items = items.Where(item => valueFilter(item.Value));
        }

        return items;
    }

}