using BLL.Cache;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public partial class CacheManagerTests
    {
        private ICacheManager<int> _cacheManager;

        [SetUp]
        public void Init()
        {
            var cacheDictionaty = new CacheDictionary<int>();
            _cacheManager = new CacheManager<int>(cacheDictionaty);

            _cacheManager.Add(Internals.InitCacheItem);
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

        [Test]
        public void Add_IntValue_ReturnTrue()
        {
            var actual =_cacheManager.Add("newKey", 1234);

            Assert.AreEqual(true, actual);
        }

        [Test]
        public void Add_ExistingKey_ReturnFalse()
        {
            var actual = _cacheManager.Add(Internals.INIT_KEY, 1234);

            Assert.AreEqual(false, actual);
        }

        [TestCase("firstKey", 768907445)]
        [TestCase("secondKey", -768907445)]
        [TestCase("thirdKey", int.MinValue)]
        [TestCase("fourthKey", int.MaxValue)]
        public void Add_CachItem_ItemAdded(string key, int value)
        {
            var expected = new CacheItem<int>(key, value);
            _cacheManager.Add(expected);

            var actual = _cacheManager.GetCacheItem(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(Internals.INIT_KEY, 768907445)]
        [TestCase(Internals.INIT_KEY, -768907445)]
        [TestCase(Internals.INIT_KEY, int.MinValue)]
        [TestCase(Internals.INIT_KEY, int.MaxValue)]
        public void Update_IntValue_ValueAdded(string key, int updatedValue)
        {
            _cacheManager.Update(key, updatedValue);

            var actual = _cacheManager.Get(key);

            Assert.AreEqual(updatedValue, actual);
        }
        
        [TestCase(Internals.INIT_KEY, 33)]
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
            => _cacheManager.IsExists(key);
        #endregion
    }
}
