using System;
using Bus;
using Bus.Abstractions;
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
            
            var textCreatedHandler = new VowelConsCountedHandler(keyValueStorage);
            bus.Subscribe<VowelConsCounted, VowelConsCountedHandler>(busName: "VowelConsRater", messageHandler: textCreatedHandler);
            Console.ReadLine();
        }
    }
}
