using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using KeyValueStorage;

namespace IntegrationEvent
{
    public class RedisEventBus : IEventBus
    {
        private const string EventChannelName = "events";
        private const string EventNamePrefix = "Event_";
        private readonly IKeyValueStorage _storage;
        public RedisEventBus(IKeyValueStorage storage)
        {
            _storage = storage;
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>, new()
        {
            Subscribe<TEvent, TEventHandler>(new TEventHandler());
        }

        public void Subscribe<TEvent, TEventHandler>(TEventHandler eventHandler)
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>
        {
            Type eventType = typeof(TEvent);
            string observableEventName = EventNamePrefix + eventType.Name;

            _storage.Subscribe(EventChannelName, (message) =>
            {
                string eventNameFromBus = message.Substring(0, observableEventName.Length);
                if (eventNameFromBus != observableEventName)
                {
                    return;
                }

                string eventBody = message.Substring(observableEventName.Length);
                TEvent @event = JsonConvert.DeserializeObject<TEvent>(eventBody);
                eventHandler.Handle(@event);
            });
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            string eventBody = JsonConvert.SerializeObject(@event);
            Type eventType = typeof(TEvent);
            string fullEvent = $"{EventNamePrefix}{eventType.Name}{eventBody}";

            _storage.Publish(EventChannelName, fullEvent);
        }
    }
}