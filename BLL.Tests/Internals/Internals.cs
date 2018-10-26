using System;

namespace BLL.Tests
{
    internal static class Internals
    {
        public const string INIT_KEY = "testKey";
        public const int INT_INIT_VALUE = 1234;
        public const string STR_INIT_VALUE = "testValue";

        public const string NONEXISTENT_KEY = "nonExistentKey";

        public const string NEW_KEY = "newKey";
        public const int NEW_INT_VALUE = 12345;
        public const string NEW_STR_VALUE = "newValue";

        public const int INIT_DAYS = 1;
        public const int INIT_HOURS = 0;
        public const int INIT_MINUTES = 0;
        public const int SECONDS = 5;

        public const int THREAD_TIMEOUT = 11_000; 

        public static TimeSpan InitTimeSpan => new TimeSpan(INIT_DAYS, INIT_HOURS, INIT_MINUTES);
    }
}
