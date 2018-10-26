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
            _cacheDictionary = cacheDictionary ?? 
                throw new ArgumentNullException(nameof(cacheDictionary));
        }
        
        public override void Clear()
            => _cacheDictionary.Clear();

        public override bool IsExists(string key)
            => _cacheDictionary.IsExists(key);


        public bool AddOrUpdate(string key, TCacheValue value)
        {
            var item = new CacheItem<TCacheValue>(key, value);

            return AddOrUpdate(item);
        }

        public bool AddOrUpdate(CacheItem<TCacheValue> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return (GetCacheItem(item.Key)) != null ? Update(item) : Add(item);
        }

        protected override bool AddItem(CacheItem<TCacheValue> item)
        {
            return _cacheDictionary.Add(item);
        }
        
        protected override CacheItem<TCacheValue> GetItem(string key)
        {
            return _cacheDictionary.Get(key);
        }

        protected override bool UpdateItem(CacheItem<TCacheValue> item)
        {
            _cacheDictionary.Update(item);

            return _cacheDictionary.Get(item.Key) == item;
        }

        protected override bool DeleteItem(string key)
        {
            return _cacheDictionary.Delete(key);
        }
    }
}
