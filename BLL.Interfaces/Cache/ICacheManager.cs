using BLL.Interfaces.DTO;

namespace BLL.Interfaces.Cache
{
    public interface ICacheManager<TCacheValue> : ICache<TCacheValue>
    {
        bool AddOrUpdate(string key, TCacheValue value);

        bool AddOrUpdate(CacheItem<TCacheValue> item);
    }
}
