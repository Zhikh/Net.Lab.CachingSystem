using BLL.Cache;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BLL.Tests
{
    [TestFixture]
    public partial class CacheManagerTests
    {
        private ICacheManager<int> _cacheManager;
        private List<CacheItem<int>> _fakeCache;

        [SetUp]
        public void Init()
        {
            var cacheItem = new CacheItem<int>(TestData.INIT_KEY, TestData.INT_INIT_VALUE,
                                               TestData.InitTimeSpan);
            _fakeCache = new List<CacheItem<int>>();
            _cacheManager = new CacheManager<int>(MockObject.CreateCacheDictionary(_fakeCache));

            _cacheManager.Add(cacheItem);
        }

        #region Exceptions
        [Test]
        public void Add_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheManager.Add(null, TestData.INT_INIT_VALUE));

        [Test]
        public void Add_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheManager.Add(string.Empty, TestData.INT_INIT_VALUE));

        [Test]
        public void AddOrUpdate_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheManager.AddOrUpdate(null, TestData.INT_INIT_VALUE));

        [Test]
        public void AddOrUpdate_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheManager.AddOrUpdate(string.Empty, TestData.INT_INIT_VALUE));

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
                => _cacheManager.Update(null, TestData.INT_INIT_VALUE));

        [Test]
        public void Update_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheManager.Update(string.Empty, TestData.INT_INIT_VALUE));

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
                var item = _cacheManager[null];
            });

        [Test]
        public void Indexer_EmptyKey_ArgumentNullException()
            => Assert.Catch<ArgumentException>(() =>
            {
                var item = _cacheManager[string.Empty];
            });
        #endregion

        #region General
        [TestCase("firstKey", 768907445)]
        [TestCase("secondKey", -768907445)]
        [TestCase("thirdKey", int.MinValue)]
        [TestCase("fourthKey", int.MaxValue)]
        public void Add_IntValue_ValueAdded(string key, int value)
        {
            _cacheManager.Add(key, value);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(value, actual);
        }
        
        [TestCase("firstKey", 567882)]
        [TestCase("secondKey", -1346760)]
        [TestCase("thirdKey", int.MinValue)]
        [TestCase("fourthKey", int.MaxValue)]
        public void Add_CachItem_ItemAdded(string key, int value)
        {
            var expected = new CacheItem<int>(key, value);
            _cacheManager.Add(expected);

            var actual = _cacheManager.GetCacheItem(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestData.INIT_KEY, TestData.NEW_INT_VALUE)]
        [TestCase("firstKey", 5678739)]
        [TestCase("secondKey", -1460)]
        [TestCase("thirdKey", int.MinValue)]
        [TestCase("fourthKey", int.MaxValue)]
        public void AddOrUpdate_CachItem_ItemAddedOrUpdated(string key, int value)
        {
            var expected = new CacheItem<int>(key, value);
            _cacheManager.AddOrUpdate(expected);

            var actual = _cacheManager.GetCacheItem(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestData.INIT_KEY, TestData.NEW_INT_VALUE)]
        [TestCase("firstKey", 479)]
        [TestCase("secondKey", -88888)]
        [TestCase("thirdKey", int.MinValue)]
        [TestCase("fourthKey", int.MaxValue)]
        public void AddOrUpdate_IntValue_ItemAddedOrUpdated(string key, int value)
        {
            _cacheManager.AddOrUpdate(key, value);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(value, actual);
        }

        [TestCase(TestData.INIT_KEY, 76890744)]
        [TestCase(TestData.INIT_KEY, -76890745)]
        [TestCase(TestData.INIT_KEY, int.MinValue)]
        [TestCase(TestData.INIT_KEY, int.MaxValue)]
        public void Update_IntValue_ValueUpdated(string key, int updatedValue)
        {
            _cacheManager.Update(key, updatedValue);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(updatedValue, actual);
        }

        [TestCase(TestData.INIT_KEY, 12)]
        [TestCase(TestData.INIT_KEY, 123)]
        public void Update_TimeOut_ItemUpdated(string key, int updatedValue)
        {
            var expected = _cacheManager.GetCacheItem(key);
            expected.ExpirationTimeout = new TimeSpan(updatedValue);
            _cacheManager.Update(expected);

            var actual = _cacheManager.GetCacheItem(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestData.INIT_KEY, TestData.NEW_INT_VALUE)]
        public void Update_CachItem_ItemUpdated(string key, int updatedValue)
        {
            var expected = new CacheItem<int>(key, updatedValue);
            _cacheManager.Update(expected);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(expected.Value, actual);
        }

        [TestCase(TestData.INIT_KEY)]
        public void Delete_Key_ValueDeleted(string key)
        {
            _cacheManager.Delete(key);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(default(int), actual);
        }

        [TestCase(TestData.INIT_KEY, ExpectedResult = true)]
        [TestCase(TestData.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool Delete_Key_ReturnsBoolValue(string key) 
            => _cacheManager.Delete(key);

        [TestCase(TestData.INIT_KEY, ExpectedResult = true)]
        [TestCase(TestData.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool IsExists_Key_ReturnsBoolValue(string key)
            => _cacheManager.IsExist(key);
        #endregion
    }
}
