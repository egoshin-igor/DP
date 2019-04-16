using System;
using Microsoft.Extensions.DependencyInjection;
using Bus.Abstractions;

namespace IntegrationEvent
{
    public class EventBus : IEventBus
    {
        private const string EventChannelName = "events";
        private readonly IBus _bus;
        public EventBus(IBus bus)
        {
            _bus = bus;
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IntegrationEventHandler<TEvent>, new()
        {
            Subscribe<TEvent, TEventHandler>(new TEventHandler());
        }

        public void Subscribe<TEvent, TEventHandler>(TEventHandler eventHandler)
            where TEvent : IntegrationEvent
            where TEventHandler : IntegrationEventHandler<TEvent>
        {
            _bus.Subscribe<TEvent, TEventHandler>(EventChannelName, eventHandler);
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        {
            _bus.Publish(EventChannelName, @event);
        }

        public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IntegrationEvent
        {
            _bus.Subscribe<TEvent>(EventChannelName, action);
        }
    }
}