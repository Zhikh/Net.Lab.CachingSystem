using System;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;

namespace BLL.Cache
{
    public abstract class BaseCache<TCacheValue> : ICache<TCacheValue>
    {
        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool Add(string key, TCacheValue value)
        {
            var item = new CacheItem<TCacheValue>(key, value);

            return Add(item);
        }

        /// <inheritdoc/>
        public bool Add(CacheItem<TCacheValue> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return AddItem(item);
        }

        /// <inheritdoc/>
        public TCacheValue Get(string key)
        {
            var item = GetCacheItem(key);

            if (item != null && item.Key.Equals(key))
            {
                return item.Value;
            }

            return default(TCacheValue);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool Update(string key, TCacheValue value)
        {
            var item = new CacheItem<TCacheValue>(key, value);

            return Update(item);
        }

        /// <inheritdoc/>
        public bool Update(CacheItem<TCacheValue> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateItem(item);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public abstract void Clear();

        /// <inheritdoc/>
        public abstract bool IsExist(string key);

        protected abstract bool AddItem(CacheItem<TCacheValue> item);

        protected abstract CacheItem<TCacheValue> GetItem(string key);

        protected abstract bool UpdateItem(CacheItem<TCacheValue> item);

        protected abstract bool DeleteItem(string key);
    }
}
