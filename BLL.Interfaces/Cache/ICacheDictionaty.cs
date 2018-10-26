using System;
using BLL.Interfaces.DTO;

namespace BLL.Interfaces.Cache
{
    public interface ICacheDictionaty<TCacheValue>
    {
        /// <summary>
        /// Count of elements in dictionary.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        void Clear();

        /// <summary>
        /// Checks in dictionary existence of item with this key.
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
        /// Adds a <see cref="CacheItem"/> to the cache.
        /// </summary>
        /// <param name="item"> Item for saving in cahce. </param>
        /// <returns> True, if item is added, otherwise - false.</returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="item"/> is null.
        /// </exception>
        bool Add(CacheItem<TCacheValue> item);

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
        CacheItem<TCacheValue> Get(string key);

        /// <summary>
        /// Updates cache item in cache for this key.
        /// </summary>
        /// <param name="item"> Item for updated. </param>
        /// <returns> True, if item is updates, otherwise - false. </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="item"/> is null.
        /// </exception>
        void Update(CacheItem<TCacheValue> item);

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
