using System;
using StackExchange.Redis;

namespace TextListener
{
    public class RedisStore
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;
    }
}