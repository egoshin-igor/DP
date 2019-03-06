namespace IntegrationEvent
{
    public interface IIntegrationEventHandler<TEvent> where TEvent: IntegrationEvent
    {
        void Handle(TEvent @event);
    }
}