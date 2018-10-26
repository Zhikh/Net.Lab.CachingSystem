using System;
using BLL.Interfaces.DTO;

namespace BLL.Interfaces.Cache
{
    public interface ICache<TCacheValue>
    {
        /// <summary>
        /// Gets or sets value by key.
        /// </summary>
        /// <param name="key"> The key of value in cache. </param>
        /// <returns> The value for this key. </returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="key"/> is null.</exception>
        TCacheValue this[string key] { get; set; }

        /// <summary>
        /// Adds a value for key in cache.
        /// </summary>
        /// <param name="key"> The key of value in cache. </param>
        /// <param name="value"> The value for saving in cache. </param>
        /// <returns> True, if pair of key and value is added, otherwise - false.</returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="key"/> or <paramref name="value"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="key"/> is empty.
        /// </exception>
        bool Add(string key, TCacheValue value);

        /// <summary>
        /// Adds a <see cref="CacheItem"/> to the cache.
        /// </summary>
        /// <param name="item"> Item for saving in cahce. </param>
        /// <returns> True, if item is added, otherwise - false.</returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="item"/> is null.
        /// </exception>
        bool Add(CacheItem<TCacheValue> item);

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Checks in cache existence of item with this key.
        /// </summary>
        /// <param name="key"> The cache key to check. </param>
        /// <returns> True, if item exists, otherwise - false. </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="key"/> is empty.
        /// </exception>
        bool IsExist(string key);

        /// <summary>
        /// Gets a value for this key.
        /// </summary>
        /// <param name="key"> The key for getting value from cache. </param>
        /// <returns> The value for this key. </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="key"/> is empty.
        /// </exception>
        TCacheValue Get(string key);

        /// <summary>
        /// Gets the instance of <see cref="CacheItem"/> for this key
        /// </summary>
        /// <param name="key"> The key for getting value from cache. </param>
        /// <returns> The instance of <see cref="CacheItem"/> for this key. </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="key"/> is empty.
        /// </exception>
        CacheItem<TCacheValue> GetCacheItem(string key);

        /// <summary>
        /// Updates value in cache for this key.
        /// </summary>
        /// <param name="key"> The key for updating value in cache. </param>
        /// <param name="value"> Updated value. </param>
        /// <returns> True, if value is updates, otherwise - false. </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="key"/> is empty.
        /// </exception>
        bool Update(string key, TCacheValue value);

        /// <summary>
        /// Updates cache item in cache for this key.
        /// </summary>
        /// <param name="item"> Item for updated. </param>
        /// <returns> True, if item is updates, otherwise - false. </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="item"/> is null.
        /// </exception>
        bool Update(CacheItem<TCacheValue> item);

        /// <summary>
        /// Removes pair of key and value.
        /// </summary>
        /// <param name="key"> Key for removing. </param>
        /// <returns> True, if cache item is removed, otherwise - false. </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the <paramref name="key"/> is empty.
        /// </exception>
        bool Delete(string key);
    }
}
