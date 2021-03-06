namespace IntegrationEvent
{
    public interface IEventBus
    {
        void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>, new();

        void Subscribe<TEvent, TEventHandler>(TEventHandler eventHandler)
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;

        void Publish<TEvent>(TEvent @event)
            where TEvent : IntegrationEvent;
    }
}