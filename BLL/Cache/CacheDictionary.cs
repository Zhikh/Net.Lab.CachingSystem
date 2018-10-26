﻿using System;
using System.Collections.Concurrent;
using System.Threading;
using BLL.Interfaces.Cache;
using BLL.Interfaces.DTO;

namespace BLL.Cache
{
    public sealed class CacheDictionary<TCacheValue> : ICacheDictionaty<TCacheValue>
    {
        private const int INTERVAL = 5000;
        
        private readonly ConcurrentDictionary<string, CacheItem<TCacheValue>> _dictionary;
        private readonly Timer _timer;

        private int _location;

        public CacheDictionary()
        {
            _dictionary = new ConcurrentDictionary<string, CacheItem<TCacheValue>>();
            _timer = new Timer(CheckOldItems, null, INTERVAL, INTERVAL);
        }
        
        public int Count => _dictionary.Count;

        public void Clear() => _dictionary.Clear();

        public bool IsExists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key value can't be null or Empty", nameof(key));
            }

            return _dictionary.ContainsKey(key);
        }

        public bool Add(CacheItem<TCacheValue> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return _dictionary.TryAdd(item.Key, item);
        }

        public CacheItem<TCacheValue> Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key == string.Empty)
            {
                throw new ArgumentException("The key value can't be empty!", nameof(key));
            }

            if (_dictionary.TryGetValue(key, out CacheItem<TCacheValue> result))
            {
                if (result.IsExpired)
                {
                    _dictionary.TryRemove(key, out CacheItem<TCacheValue> removeResult);

                    return null;
                }
            }

            return result;
        }

        public void Update(CacheItem<TCacheValue> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            _dictionary[item.Key] = item;
        }

        public bool Delete(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key == string.Empty)
            {
                throw new ArgumentException("The key value can't be empty!", nameof(key));
            }

            return _dictionary.TryRemove(key, out CacheItem<TCacheValue> val);
        }

        private void CheckOldItems(object state)
        {
            if (_location > 0)
            {
                return;
            }

            if (Interlocked.CompareExchange(ref _location, 1, 0) == 0)
            {
                try
                {
                    RemoveOldItems();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error occurred during cheching on old elements.", ex);
                }
                finally
                {
                    Interlocked.Exchange(ref _location, 0);
                }
            }
        }

        private void RemoveOldItems()
        {
            foreach (var item in _dictionary.Values)
            {
                if (item.IsExpired)
                {
                    Delete(item.Key);
                }
            }
        }
    }
}
