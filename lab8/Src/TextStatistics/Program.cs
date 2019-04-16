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
            var textRankCalculatedHandler = new TextRankCalculatedHandler(keyValueStorage);
            var processingAcceptedCount = new ProcessingAcceptedHandler(keyValueStorage);

            eventBus.Subscribe<TextRankCalculated, TextRankCalculatedHandler>(textRankCalculatedHandler);
            eventBus.Subscribe<ProcessingAccepted, ProcessingAcceptedHandler>(processingAcceptedCount);
            Console.ReadLine();
        }
    }
}
