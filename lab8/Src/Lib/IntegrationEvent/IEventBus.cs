using System;

namespace IntegrationEvent
{
    public interface IEventBus
    {
        void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IntegrationEventHandler<TEvent>, new();

        void Subscribe<TEvent, TEventHandler>(TEventHandler eventHandler)
            where TEvent : IntegrationEvent
            where TEventHandler : IntegrationEventHandler<TEvent>;

        void Subscribe<TEvent>(Action<TEvent> action)
            where TEvent : IntegrationEvent;

        void Publish<TEvent>(TEvent @event)
            where TEvent : IntegrationEvent;
    }
}