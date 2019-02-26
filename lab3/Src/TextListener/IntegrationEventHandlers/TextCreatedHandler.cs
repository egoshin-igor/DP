using System;
using IntegrationEvent;
using StackExchange.Redis;
using TextListener.IntegrationEvents;

namespace TextListener.IntegrationEventHandlers
{
    public class TextCreatedHandler : IIntegrationEventHandler<TextCreated>
    {
        public void Handle(TextCreated @event)
        {
            ConnectionMultiplexer redis = RedisStore.Connection;
            IDatabase redisDatabase = redis.GetDatabase();
            
            Console.WriteLine($"TextId: {@event.TextId}, Text: {redisDatabase.StringGet(@event.TextId)}");
        }
    }
}