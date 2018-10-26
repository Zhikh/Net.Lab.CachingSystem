using System;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;

namespace BLL.Cache
{
    public abstract class BaseCache<TCacheValue> : ICache<TCacheValue>
    {
        public TCacheValue this[string key]
        {
            get
            {
                return Get(key);
            }

            set
            {
                Update(key, value);
            }
        }

        public bool Add(string key, TCacheValue value)
        {
            var item = new CacheItem<TCacheValue>(key, value);

            return Add(item);
        }

        public bool Add(CacheItem<TCacheValue> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return AddItem(item);
        }
        
        public TCacheValue Get(string key)
        {
            var item = GetCacheItem(key);

            if (item != null && item.Key.Equals(key))
            {
                return item.Value;
            }

            return default(TCacheValue);
        }

        public CacheItem<TCacheValue> GetCacheItem(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key == string.Empty)
            {
                throw new ArgumentException("The parameter key can't be empty!", nameof(key));
            }

            return GetItem(key);
        }

        public bool Update(string key, TCacheValue value)
        {
            var item = new CacheItem<TCacheValue>(key, value);

            return Update(item);
        }
        
        public bool Update(CacheItem<TCacheValue> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateItem(item);
        }

        public bool Delete(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key == string.Empty)
            {
                throw new ArgumentException("The parameter key can't be empty!", nameof(key));
            }

            return DeleteItem(key);
        }
        
        public abstract void Clear();

        public abstract bool IsExists(string key);

        protected abstract bool AddItem(CacheItem<TCacheValue> item);

        protected abstract CacheItem<TCacheValue> GetItem(string key);

        protected abstract bool UpdateItem(CacheItem<TCacheValue> item);

        protected abstract bool DeleteItem(string key);
    }
}
