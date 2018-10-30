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
            var cacheItem = new CacheItem<int>(TestData.INIT_KEY, TestData.INT_INIT_VALUE,
                                               TestData.InitTimeSpan);
            _cacheDictionaty = new CacheDictionary<int>();
            
            _cacheDictionaty.Add(cacheItem);
        }

        #region Exceptions
        [Test]
        public void Add_NullCacheItem_ArgumentNullException()
           => Assert.Catch<ArgumentNullException>(()
               => _cacheDictionaty.Add(null));

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

        [Test]
        public void IsExist_NullKey_ArgumentNullException()
            => Assert.Catch<ArgumentNullException>(()
                => _cacheDictionaty.IsExist(null));

        [Test]
        public void IsExist_EmptyKey_ArgumentException()
            => Assert.Catch<ArgumentException>(()
                => _cacheDictionaty.IsExist(string.Empty));
        #endregion

        #region General
        [TestCase("firstKey", 65748)]
        [TestCase("firstKey", -65748)]
        [TestCase("secondKey", int.MinValue)]
        [TestCase("thirdKey", int.MaxValue)]
        public void Add_CacheItem_ItemAdded(string key, int value)
        {
            var expected = new CacheItem<int>(key, value, TestData.InitTimeSpan);
            _cacheDictionaty.Add(expected);

            var actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestData.INIT_KEY, 12345)]
        [TestCase(TestData.INIT_KEY, -12345)]
        [TestCase(TestData.INIT_KEY, int.MinValue)]
        [TestCase(TestData.INIT_KEY, int.MaxValue)]
        public void Update_CacheItem_ItemUpdated(string key, int updatedValue)
        {
            var expected = new CacheItem<int>(key, updatedValue, TestData.InitTimeSpan);
            _cacheDictionaty.Update(expected);

            var actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestCase(TestData.INIT_KEY)]
        public void Delete_Key_ItemDeleted(string key)
        {
            _cacheDictionaty.Delete(key);
            
            CacheItem<int> actual = _cacheDictionaty.Get(key);

            Assert.AreEqual(null, actual);
        }

        [TestCase(TestData.INIT_KEY, ExpectedResult = true)]
        [TestCase(TestData.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool Delete_Key_ReturnsBoolValue(string key) 
            => _cacheDictionaty.Delete(key);

        [TestCase(TestData.INIT_KEY, ExpectedResult = true)]
        [TestCase(TestData.NONEXISTENT_KEY, ExpectedResult = false)]
        public bool IsExsists_Key_ReturnsBoolValue(string key)
            => _cacheDictionaty.IsExist(key);

        [Test]
        public void Clear_Key_CacheDictionaryCleared()
        {
            var item = new CacheItem<int>(TestData.NEW_KEY, TestData.NEW_INT_VALUE,
                                          TestData.InitTimeSpan);

            _cacheDictionaty.Add(item);
            _cacheDictionaty.Clear();

            var count = _cacheDictionaty.Count;

            Assert.AreEqual(0, count);
        }

        [TestCase(10)]
        [TestCase(10_000)]
        public void Count_GetsCountOfValuesInDictionary(int n)
        {
            _cacheDictionaty.Clear();

            for (int i = 0; i < n; i++)
            {
                _cacheDictionaty.Add(new CacheItem<int>(i.ToString(), i, TestData.InitTimeSpan));
            }

            var count = _cacheDictionaty.Count;

            Assert.AreEqual(n, count);
        }

        [Test]
        public void RemoveOldItems_LifeTimeOutOfCacheItem_CachItemDeleted()
        {
            var timeOut = new TimeSpan(TestData.INIT_HOURS, TestData.INIT_MINUTES, 
                                       TestData.SECONDS);
            var item = new CacheItem<int>(TestData.NEW_KEY, TestData.NEW_INT_VALUE, timeOut);

            _cacheDictionaty.Add(item);

            Thread.Sleep(TestData.THREAD_TIMEOUT);

            var actual = _cacheDictionaty.Get(TestData.NEW_KEY);

            Assert.AreEqual(null, actual);
        }
        #endregion
    }
}
