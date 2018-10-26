using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;
using Moq;

namespace BLL.Tests
{
    public static class MockObject
    {
        public static ICacheDictionaty<T> CreateCacheDictionary<T>(List<CacheItem<T>> data)
        {
            var mock = new Mock<ICacheDictionaty<T>>();

            mock.Setup(x => x.Add(It.IsAny<CacheItem<T>>()))
                .Callback(new Action<CacheItem<T>>(x =>
                {
                    data.Add(x);
                }))
                .Returns(true);

            mock.Setup(x => x.Update(It.IsAny<CacheItem<T>>()))
                .Callback(new Action<CacheItem<T>>(x =>
                {
                    var i = data.FindIndex(q => q.Key.Equals(x.Key));
                    data[i] = x;
                }));

            mock.Setup(x => x.Delete(It.IsAny<string>()))
                .Returns((string key) =>
                {
                    var item = data.Find(q => q.Key.Equals(key));
                    return data.Remove(item);
                });

            mock.Setup(x => x.Get(It.IsAny<string>()))
                .Returns((string key) => data.FirstOrDefault(
                x => x.Key == key));

            mock.Setup(x => x.IsExist(It.IsAny<string>()))
                .Returns((string key) => data.Exists(
                x => x.Key == key));

            return mock.Object;
        }
    }
}
