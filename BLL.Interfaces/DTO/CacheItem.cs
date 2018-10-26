using System;

namespace BLL.Interfaces.DTO
{
    public sealed class CacheItem<T>
    {
        private const int DEFAULT_TIMEOUT = 10;

        public CacheItem(string key, T value)
           : this(value, key, null)
        {
        }

        public CacheItem(T value, string key, TimeSpan? expirationTimeout)
        {
            Value = value;  
            Key = key ?? throw new ArgumentNullException(nameof(key));

            if (ExpirationTimeout.TotalDays > 365)
            {
                throw new ArgumentOutOfRangeException(nameof(expirationTimeout), 
                    "Timeout must be less than 365.");
            }

            ExpirationTimeout = expirationTimeout ?? TimeSpan.Zero;     //?
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
                var now = DateTime.UtcNow;
                if (CreateDate.Add(ExpirationTimeout) < now)
                {
                    return true;
                }
                
                return false;
            }
        }
    }
}
