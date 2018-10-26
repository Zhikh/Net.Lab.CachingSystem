using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;
using System;

namespace BLL.Cache
{
    public sealed class CacheManager<TCacheValue> : BaseCache<TCacheValue>, ICacheManager<TCacheValue>
    {
        private readonly ICacheDictionaty<TCacheValue> _cacheDictionary;

        public CacheManager(ICacheDictionaty<TCacheValue> cacheDictionary)
        {
            _cacheDictionary = cacheDictionary ?? throw new ArgumentNullException(nameof(cacheDictionary));
        }

        public TCacheValue AddOrUpdate(string key, TCacheValue addValue, Func<TCacheValue, TCacheValue> updateValue)
        {
            throw new NotImplementedException();
        }

        public TCacheValue AddOrUpdate(CacheItem<TCacheValue> addItem, Func<TCacheValue, TCacheValue> updateValue)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
            => _cacheDictionary.Clear();

        public override bool IsExists(string key)
            => _cacheDictionary.IsExists(key);
        
        public void SetTimeout(string key, TimeSpan timeout)
        {
            var item = _cacheDictionary.Get(key);

            item.ExpirationTimeout = timeout;
        }
        
        protected override bool AddItem(CacheItem<TCacheValue> item)
        {
            return _cacheDictionary.Add(item);
        }
        
        protected override CacheItem<TCacheValue> GetItem(string key)
        {
            return _cacheDictionary.Get(key);
        }

        protected override void UpdateItem(CacheItem<TCacheValue> item)
        {
            _cacheDictionary.Update(item);
        }

        protected override bool DeleteItem(string key)
        {
            return _cacheDictionary.Delete(key);
        }
    }
}
