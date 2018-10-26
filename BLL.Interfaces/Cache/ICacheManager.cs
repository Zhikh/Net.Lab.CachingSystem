﻿using BLL.Interfaces.DTO;
using System;

namespace BLL.Interfaces.Cache
{
    public interface ICacheManager<TCacheValue> : ICache<TCacheValue>
    {
        TCacheValue AddOrUpdate(string key, TCacheValue addValue, Func<TCacheValue, TCacheValue> updateValue);

        TCacheValue AddOrUpdate(CacheItem<TCacheValue> addItem, Func<TCacheValue, TCacheValue> updateValue);

        TCacheValue GetOrAdd(string key, TCacheValue value);

        TCacheValue Update(string key, Func<TCacheValue, TCacheValue> updateValue);

        void SetTimeout(string key, DateTimeOffset timeout);

        void SetTimeout(string key, TimeSpan timeout);
    }
}