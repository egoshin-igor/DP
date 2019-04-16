using System;
using Bus;
using Bus.Abstractions;
using IntegrationEvent;
using KeyValueStorage;
using KeyValueStorage.Abstractions;
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
            IBus bus = new BaseBus(keyValueStorage); 
            IEventBus eventBus = new EventBus(bus);
            var textCreatedHandler = new TextCreatedHandler( keyValueStorage );
            
            eventBus.Subscribe<TextCreated, TextCreatedHandler>( textCreatedHandler );
            Console.ReadLine();
        }
    }
}
