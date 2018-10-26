using BLL.Interfaces.DTO;
using System;

namespace BLL.Interfaces.Cache
{
    public interface ICacheManager<TCacheValue> : ICache<TCacheValue>
    {
        TCacheValue AddOrUpdate(string key, TCacheValue addValue, Func<TCacheValue, TCacheValue> updateValue);

        TCacheValue AddOrUpdate(CacheItem<TCacheValue> addItem, Func<TCacheValue, TCacheValue> updateValue);
        
        void SetTimeout(string key, TimeSpan timeout);
    }
}
