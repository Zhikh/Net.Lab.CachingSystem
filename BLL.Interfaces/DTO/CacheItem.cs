using System;

namespace BLL.Interfaces.DTO
{
    public sealed class CacheItem<T>
    {
        public CacheItem(string key, T value)
           : this(value, key, null)
        {
        }

        public CacheItem(T value, string key, TimeSpan? expirationTimeout)
        {
            Value = value;  //TODO checking?
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ValueType = value.GetType();

            if (ExpirationTimeout.TotalDays > 365)
            {
                throw new ArgumentOutOfRangeException(nameof(expirationTimeout), 
                    "Expiration timeout must less than 365.");
            }

            ExpirationTimeout = expirationTimeout ?? new TimeSpan();
            CreateDate = DateTime.Now;
        }

        public T Value { get; }

        public string Key { get; }

        public Type ValueType { get; }

        public TimeSpan ExpirationTimeout { get; }

        public DateTime CreateDate { get; }

        public bool IsExsists
        {
            get
            {
                //TODO add implementation

                return false;
            }
        }
    }
}
