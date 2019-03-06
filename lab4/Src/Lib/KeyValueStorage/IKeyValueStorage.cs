using System;

namespace KeyValueStorage
{
    public interface IKeyValueStorage
    {
        void Subscribe(string channel, Action<string> action);
        void Publish(string channel, string message);
        void Set(string key, string value);
        string Get(string key, int retryCount = 1);
    }
}
