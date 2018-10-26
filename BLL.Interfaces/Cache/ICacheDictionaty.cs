using BLL.Interfaces.DTO;

namespace BLL.Interfaces.Cache
{
    public interface ICacheDictionaty<TCacheValue>
    {
        int Count { get; }

        void Clear();

        bool IsExists(string key);

        bool Add(CacheItem<TCacheValue> item);

        CacheItem<TCacheValue> Get(string key);

        void Update(CacheItem<TCacheValue> item);

        bool Delete(string key);
    }
}
