using System;

namespace KeyValueStorage.Abstractions
{
    public interface IKeyValueStorage
    {
        void Subscribe(string channel, Action<string> action);
        void Publish(string channel, string message);
        void Set(string key, string value);
        string Get(string key, int retryCount = 1);
        void AddMessageToQueue(string queueName, string message);
        string GetMessageFromQueue(string queueName);
    }
}
