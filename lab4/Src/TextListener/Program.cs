using System;
using IntegrationEvent;
using KeyValueStorage;
using Newtonsoft.Json;
using StackExchange.Redis;
using TextListener.IntegrationEventHandlers;
using TextListener.IntegrationEvents;

namespace TextListener
{
    class Program
    {
        static void Main(string[] args)
        {
            IKeyValueStorage keyValueStorage = new RedisStore();
            IEventBus eventBus = new RedisEventBus(keyValueStorage);
            var textCreatedHandler = new TextCreatedHandler( keyValueStorage );
            
            eventBus.Subscribe<TextCreated, TextCreatedHandler>( textCreatedHandler );
            Console.ReadLine();
        }
    }
}
