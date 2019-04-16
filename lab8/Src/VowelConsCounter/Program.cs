using System;
using Bus;
using Bus.Abstractions;
using KeyValueStorage;
using KeyValueStorage.Abstractions;
using VowelConsCounter.BusMessageHandlers;
using VowelConsCounter.BusMessages;

namespace VowelConsCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            IKeyValueStorage keyValueStorage = new RedisStore();
            IBus bus = new BaseBus(keyValueStorage);
            
            var textCreatedHandler = new TextCreatedHandler(keyValueStorage, bus);
            bus.Subscribe<TextCreated, TextCreatedHandler>(busName: "VowelConsCounter", messageHandler: textCreatedHandler);
            Console.ReadLine();
        }
    }
}
