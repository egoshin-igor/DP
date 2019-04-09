using System;
using Bus;
using Bus.Abstractions;
using IntegrationEvent;
using KeyValueStorage;
using KeyValueStorage.Abstractions;
using VowelConsRater.BusMessageHandlers;

namespace VowelConsRater
{
    class Program
    {
        static void Main(string[] args)
        {
            IKeyValueStorage keyValueStorage = new RedisStore();
            IBus bus = new BaseBus(keyValueStorage);
            IEventBus eventBus = new EventBus(bus);
            
            var textCreatedHandler = new VowelConsCountedHandler(keyValueStorage, eventBus);
            bus.Subscribe<VowelConsCounted, VowelConsCountedHandler>(busName: "VowelConsRater", messageHandler: textCreatedHandler);
            Console.ReadLine();
        }
    }
}
