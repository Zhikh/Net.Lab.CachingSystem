using System;
using System.Threading;
using BLL.Cache;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public partial class CacheDictionaryTests
    {
        private ICacheDictionaty<int> _cacheDictionaty;

        [SetUp]
        public void Init()
        {
            var cacheItem = new CacheItem<int>(Internals.INIT_KEY, Internals.INT_INIT_VALUE,
                                               Internals.InitTimeSpan);
            _cacheDictionaty = new CacheDictionary<int>();
            
            _cacheDictionaty.Add(cacheItem);
        }
        
        #region General
        [TestCase("firstKey", 65748)]
        [TestCase("firstKey", -65748)]
        [TestCase("secondKey", int.MinValue)]
        [TestCase("thirdKey", int.MaxValue)]
        public void Add_CacheItem_ItemAdded(string key, int value)
        {
            var expected = new CacheItem<int>(key, value, Internals.InitTimeSpan);
            _cacheDictionaty.Add(expected);

            var actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(Internals.INIT_KEY, 12345)]
        [TestCase(Internals.INIT_KEY, -12345)]
        [TestCase(Internals.INIT_KEY, int.MinValue)]
        [TestCase(Internals.INIT_KEY, int.MaxValue)]
        public void Update_CacheItem_ItemUpdated(string key, int updatedValue)
        {
            var expected = new CacheItem<int>(key, updatedValue, Internals.InitTimeSpan);
            _cacheDictionaty.Update(expected);

            var actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestCase(Internals.INIT_KEY)]
        public void Delete_Key_ItemDeleted(string key)
        {
            _cacheDictionaty.Delete(key);
            
            CacheItem<int> actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(null, actual);
        }

        [TestCase(Internals.INIT_KEY, ExpectedResult = true)]
        [TestCase(Internals.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool Delete_Key_ReturnsBoolValue(string key) 
            => _cacheDictionaty.Delete(key);

        [TestCase(Internals.INIT_KEY, ExpectedResult = true)]
        [TestCase(Internals.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool IsExsists_Key_ReturnsBoolValue(string key)
            => _cacheDictionaty.IsExist(key);

        [Test]
        public void Clear_Key_EmptyCacheDictionary()
        {
            var item = new CacheItem<int>(Internals.NEW_KEY, Internals.NEW_INT_VALUE,
                                          Internals.InitTimeSpan);

            _cacheDictionaty.Add(item);
            _cacheDictionaty.Clear();

            var count = _cacheDictionaty.Count;

            Assert.AreEqual(0, count);
        }

        [TestCase(10)]
        [TestCase(10_000)]
        public void Count_GetCountOfValuesInDictionary(int n)
        {
            _cacheDictionaty.Clear();

            for (int i = 0; i < n; i++)
            {
                _cacheDictionaty.Add(new CacheItem<int>(i.ToString(), i, Internals.InitTimeSpan));
            }

            var count = _cacheDictionaty.Count;

            Assert.AreEqual(n, count);
        }

        [Test]
        public void Test()
        {
            var timeOut = new TimeSpan(Internals.INIT_HOURS, Internals.INIT_MINUTES, 
                                       Internals.SECONDS);
            var item = new CacheItem<int>(Internals.NEW_KEY, Internals.NEW_INT_VALUE, timeOut);

            _cacheDictionaty.Add(item);

            Thread.Sleep(Internals.THREAD_TIMEOUT);

            var actual = _cacheDictionaty.Get(Internals.NEW_KEY);

            Assert.AreEqual(null, actual);
        }
        #endregion
    }
}
