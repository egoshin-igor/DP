using System;
using System.Threading;
using KeyValueStorage.Abstractions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace KeyValueStorage
{
    public class RedisStore: IKeyValueStorage
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {
            var configurationOptions = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { "localhost:6379" }
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        private static ConnectionMultiplexer Connection => LazyConnection.Value;

        public string Get(string key, int retryCount = 1, int databaseNumber = 1)
        {
            string result = null;
            IDatabase redisDatabase = Connection.GetDatabase(databaseNumber);

            for (int i = 0; i < retryCount; i++)
            {
                string stringByKey = redisDatabase.StringGet(key);
                if (stringByKey != null)
                {
                    result = stringByKey;
                    break;
                }

                Thread.Sleep(millisecondsTimeout: 300);
            }

            return result;
        }

        public void Set(string key, string value, int databaseNumber = 1)
        {
            IDatabase redisDatabase = Connection.GetDatabase(databaseNumber);

            redisDatabase.StringSet(key, value);
        }

        public void Publish(string channel, string message)
        {
            ISubscriber subscriber = Connection.GetSubscriber();
            subscriber.Publish(channel, message);
        }

        public void Subscribe(string channel, Action<string> action)
        {
            ISubscriber subscriber = Connection.GetSubscriber();
            subscriber.Subscribe(channel, (redisChannel, message) => action(message));
        }

        public void AddMessageToQueue(string queueName, string message)
        {
            Connection.GetDatabase().ListLeftPush(queueName, message, flags: CommandFlags.FireAndForget);
        }

        public string GetMessageFromQueue(string queueName)
        {
            return Connection.GetDatabase().ListRightPop(queueName);
        }

        public void Set<V>(string key, V value, int databaseNumber = 1)
        {
            Set(key, JsonConvert.SerializeObject(value), databaseNumber);
        }

        public V Get<V>(string key, int retryCount = 1, int databaseNumber = 1)
        {
            string json = Get(key, retryCount, databaseNumber);
            if (json != null)
            {
                return JsonConvert.DeserializeObject<V>(json);
            }
            else
            {
                return default(V);
            }
        }
    }
}