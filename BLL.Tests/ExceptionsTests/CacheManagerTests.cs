using System;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public partial class CacheManagerTests
    {
        [Test]
        public void Add_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheManager.Add(null, Internals.INT_INIT_VALUE));

        [Test]
        public void Add_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheManager.Add(string.Empty, Internals.INT_INIT_VALUE));

        [Test]
        public void AddOrUpdate_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheManager.AddOrUpdate(null, Internals.INT_INIT_VALUE));

        [Test]
        public void AddOrUpdate_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheManager.AddOrUpdate(string.Empty, Internals.INT_INIT_VALUE));

        [Test]
        public void AddOrUpdate_NullItem_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheManager.AddOrUpdate(null));

        [Test]
        public void Get_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheManager.Get(null));

        [Test]
        public void Get_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheManager.Get(string.Empty));

        [Test]
        public void GetCacheItem_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => _cacheManager.GetCacheItem(null));

        [Test]
        public void GetCacheItem_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(() => _cacheManager.GetCacheItem(string.Empty));

        [Test]
        public void Update_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheManager.Update(null, Internals.INT_INIT_VALUE));

        [Test]
        public void Update_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheManager.Update(string.Empty, Internals.INT_INIT_VALUE));

        [Test]
        public void Delete_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => _cacheManager.Delete(null));

        [Test]
        public void Delete_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(() => _cacheManager.Delete(string.Empty));

        [Test]
        public void Indexer_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(() => 
            {
                var item =_cacheManager[null];
            });

        [Test]
        public void Indexer_EmptyKey_ArgumentNullException()
            => Assert.Catch<ArgumentException>(() =>
            {
                var item = _cacheManager[string.Empty];
            });
    }
}
