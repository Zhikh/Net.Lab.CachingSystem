using System;

namespace BLL.Interfaces.DTO
{
    public sealed class CacheItem<T>
    {
        private const int DEFAULT_TIMEOUT_DAYS = 1;

        public CacheItem(string key, T value)
           : this(key, value, null)
        {
        }

        public CacheItem(string key, T value, TimeSpan? expirationTimeout)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key == string.Empty)
            {
                throw new ArgumentException("The parameter key can't be empty!", nameof(key));
            }
            
            Value = value;  
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ExpirationTimeout = expirationTimeout ?? new TimeSpan(DEFAULT_TIMEOUT_DAYS, 0, 0);
            CreateDate = DateTime.Now;
        }

        public T Value { get; }

        public string Key { get; }

        public TimeSpan ExpirationTimeout { get; set; }

        public DateTime CreateDate { get; }

        public bool IsExpired
        {
            get
            {
                var now = DateTime.Now;
                var range = CreateDate.Add(ExpirationTimeout);

                if (range.Ticks < now.Ticks)
                {
                    return true;
                }
                
                return false;
            }
        }
    }
}
