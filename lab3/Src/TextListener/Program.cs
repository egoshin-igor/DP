using System;
using IntegrationEvent;
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
            ConnectionMultiplexer redis = RedisStore.Connection;
            RedisEventBus.Subscribe<TextCreated, TextCreatedHandler>( redis );
            Console.ReadLine();
        }
    }
}
