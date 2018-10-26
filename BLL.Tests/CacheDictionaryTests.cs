using BLL.Cache;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;
using NUnit.Framework;
using System;

namespace BLL.Tests
{
    [TestFixture]
    public partial class CacheDictionaryTests
    {
        private ICacheDictionaty<int> _cacheDictionaty;

        [SetUp]
        public void Init()
        {
            var cacheItem = new CacheItem<int>(Internals.KEY_FOR_INT_INIT_VALUE,
                Internals.INT_INIT_VALUE);
            _cacheDictionaty = new CacheDictionary<int>();
            
            _cacheDictionaty.Add(cacheItem);
        }

        #region Exceptions
        [Test]
        public void Add_NullCacheItem_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                =>_cacheDictionaty.Add(null));

        [Test]
        public void Get_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheDictionaty.Get(null));

        [Test]
        public void Get_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                 => _cacheDictionaty.Get(string.Empty));

        [Test]
        public void Update_NullCacheItem_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheDictionaty.Update(null));

        [Test]
        public void Delete_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheDictionaty.Delete(null));

        [Test]
        public void Delete_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheDictionaty.Delete(string.Empty));
        #endregion

        #region General
        [TestCase("firstKey", 65748)]
        [TestCase("firstKey", -65748)]
        [TestCase("secondKey", int.MinValue)]
        [TestCase("thirdKey", int.MaxValue)]
        public void Add_CacheItem_ItemWasAdded(string key, int value)
        {
            var expected = new CacheItem<int>(key, value);
            _cacheDictionaty.Add(expected);

            var actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(Internals.KEY_FOR_INT_INIT_VALUE, 12345)]
        [TestCase(Internals.KEY_FOR_INT_INIT_VALUE, -12345)]
        [TestCase(Internals.KEY_FOR_INT_INIT_VALUE, int.MinValue)]
        [TestCase(Internals.KEY_FOR_INT_INIT_VALUE, int.MaxValue)]
        public void Update_CacheItem_ItemWasUpdated(string key, int updatedValue)
        {
            var expected = new CacheItem<int>(key, updatedValue);
            _cacheDictionaty.Update(expected);

            var actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestCase(Internals.KEY_FOR_INT_INIT_VALUE)]
        public void Delete_Key_ItemWasDeleted(string key)
        {
            _cacheDictionaty.Delete(key);
            
            CacheItem<int> actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(null, actual);
        }
        #endregion
    }
}
