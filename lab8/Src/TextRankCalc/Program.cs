using System;
using Bus;
using Bus.Abstractions;
using IntegrationEvent;
using KeyValueStorage;
using KeyValueStorage.Abstractions;
using TextRankCalc.IntegrationEventHandlers;
using TextRankCalc.IntegrationEvents;

namespace TextRankCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            IKeyValueStorage keyValueStorage = new RedisStore();
            IBus bus = new BaseBus(keyValueStorage);
            IEventBus eventBus = new EventBus(bus);
            var textCreatedHandler = new ProcessingAcceptedHandler(keyValueStorage, bus);

            eventBus.Subscribe<ProcessingAccepted, ProcessingAcceptedHandler>(textCreatedHandler);
            Console.ReadLine();
        }
    }
}
