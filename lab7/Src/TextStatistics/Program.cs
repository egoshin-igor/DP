using System;
using Bus;
using Bus.Abstractions;
using IntegrationEvent;
using KeyValueStorage;
using KeyValueStorage.Abstractions;
using TextStatistics.IntegrationEventHandlers;
using TextStatistics.IntegrationEvents;

namespace TextStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            IKeyValueStorage keyValueStorage = new RedisStore();
            IBus bus = new BaseBus(keyValueStorage);
            IEventBus eventBus = new EventBus(bus);
            var textCreatedHandler = new TextRankCalculatedHandler(keyValueStorage);

            eventBus.Subscribe<TextRankCalculated, TextRankCalculatedHandler>(textCreatedHandler);
            Console.ReadLine();
        }
    }
}
