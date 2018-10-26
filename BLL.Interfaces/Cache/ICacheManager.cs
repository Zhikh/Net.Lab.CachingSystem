using BLL.Interfaces.DTO;

namespace BLL.Interfaces.Cache
{
    public interface ICacheManager<TCacheValue> : ICache<TCacheValue>
    {
        /// <summary>
        /// Adds or updates item.
        /// </summary>
        /// <param name="key"> Key for operation. </param>
        /// <param name="value"> Value for operation. </param>
        /// <returns> True, if element is added or updated, otherwise - false. </returns>
        bool AddOrUpdate(string key, TCacheValue value);

        /// <summary>
        /// Adds or updates item.
        /// </summary>
        /// <param name="item"> Item for operation. </param>
        /// <returns> True, if element is added or updated, otherwise - false. </returns>
        bool AddOrUpdate(CacheItem<TCacheValue> item);
    }
}
