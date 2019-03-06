using System;
using IntegrationEvent;
using KeyValueStorage;
using TextRankCalc.IntegrationEventHandlers;
using TextRankCalc.IntegrationEvents;

namespace TextRankCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            IKeyValueStorage keyValueStorage = new RedisStore();
            IEventBus eventBus = new RedisEventBus(keyValueStorage);
            var textCreatedHandler = new TextCreatedHandler(keyValueStorage);

            eventBus.Subscribe<TextCreated, TextCreatedHandler>(textCreatedHandler);
            Console.ReadLine();
        }
    }
}
