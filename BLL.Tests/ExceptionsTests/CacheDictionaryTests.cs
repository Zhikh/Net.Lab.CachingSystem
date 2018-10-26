using System;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public partial class CacheDictionaryTests
    {
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
    }
}
