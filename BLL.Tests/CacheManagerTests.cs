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
            var cacheItem = new CacheItem<int>(Internals.INIT_KEY, Internals.INT_INIT_VALUE,
                                               Internals.InitTimeSpan);
            _fakeCache = new List<CacheItem<int>>();
            _cacheManager = new CacheManager<int>(MockObject.CreateCacheDictionary(_fakeCache));

            _cacheManager.Add(cacheItem);
        }
        
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

        [TestCase(Internals.INIT_KEY, Internals.NEW_INT_VALUE)]
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

        [TestCase(Internals.INIT_KEY, Internals.NEW_INT_VALUE)]
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

        [TestCase(Internals.INIT_KEY, 76890744)]
        [TestCase(Internals.INIT_KEY, -76890745)]
        [TestCase(Internals.INIT_KEY, int.MinValue)]
        [TestCase(Internals.INIT_KEY, int.MaxValue)]
        public void Update_IntValue_ValueUpdated(string key, int updatedValue)
        {
            _cacheManager.Update(key, updatedValue);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(updatedValue, actual);
        }

        [TestCase(Internals.INIT_KEY, 12)]
        [TestCase(Internals.INIT_KEY, 123)]
        public void Update_TimeOut_ItemUpdated(string key, int updatedValue)
        {
            var expected = _cacheManager.GetCacheItem(key);
            expected.ExpirationTimeout = new TimeSpan(updatedValue);
            _cacheManager.Update(expected);

            var actual = _cacheManager.GetCacheItem(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(Internals.INIT_KEY, Internals.NEW_INT_VALUE)]
        public void Update_CachItem_ItemUpdated(string key, int updatedValue)
        {
            var expected = new CacheItem<int>(key, updatedValue);
            _cacheManager.Update(expected);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(expected.Value, actual);
        }

        [TestCase(Internals.INIT_KEY)]
        public void Delete_Key_ValueDeleted(string key)
        {
            _cacheManager.Delete(key);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(default(int), actual);
        }

        [TestCase(Internals.INIT_KEY, ExpectedResult = true)]
        [TestCase(Internals.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool Delete_Key_ReturnBoolValue(string key) 
            => _cacheManager.Delete(key);

        [TestCase(Internals.INIT_KEY, ExpectedResult = true)]
        [TestCase(Internals.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool IsExists_Key_ReturnBoolValue(string key)
            => _cacheManager.IsExist(key);
        #endregion
    }
}
