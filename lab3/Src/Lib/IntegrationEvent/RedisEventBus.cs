using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationEvent
{
    public class RedisEventBus : IEventBus
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IServiceProvider _serviceProvider;

        public RedisEventBus(ConnectionMultiplexer redis, IServiceProvider serviceProvider)
        {
            _redis = redis;
            _serviceProvider = serviceProvider;
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>
        {
            Type eventType = typeof(TEvent);

            ISubscriber subscriber = _redis.GetSubscriber();

            subscriber.Subscribe(eventType.Name, (channel, message) =>
            {
                TEventHandler eventHandler = _serviceProvider.GetService<TEventHandler>();
                TEvent @event = JsonConvert.DeserializeObject<TEvent>(message);
                eventHandler.Handle(@event);
            });
        }

        public static void Subscribe<TEvent, TEventHandler>(ConnectionMultiplexer redis)
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>, new()
        {
            Type eventType = typeof(TEvent);

            ISubscriber subscriber = redis.GetSubscriber();

            subscriber.Subscribe(eventType.Name, (channel, message) =>
            {
                TEventHandler eventHandler = new TEventHandler();
                TEvent @event = JsonConvert.DeserializeObject<TEvent>(message);
                eventHandler.Handle(@event);
            });
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            string eventJson = JsonConvert.SerializeObject(@event);
            ISubscriber subscriber = _redis.GetSubscriber();
            Type eventType = typeof(TEvent);

            subscriber.Publish(eventType.Name, eventJson);
        }
    }
}