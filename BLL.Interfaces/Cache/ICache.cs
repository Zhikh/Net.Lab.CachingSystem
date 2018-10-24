using BLL.Interfaces.DTO;

namespace BLL.Interfaces.Cache
{
    public interface ICache<TCacheValue>
    {
        TCacheValue this[string key] { get; set; }

        bool Add(string key, TCacheValue value);

        bool Add(CacheItem<TCacheValue> item);

        void Clear();

        bool Exists(string key);

        TCacheValue Get(string key);

        CacheItem<TCacheValue> GetCacheItem(string key);

        void Put(string key, TCacheValue value);

        bool Remove(string key);
    }
}
