using Bus.Abstractions;

namespace IntegrationEvent
{
    public abstract class IntegrationEventHandler<TEvent> : IBusMessageHandler<TEvent> where TEvent : IntegrationEvent
    {
        public abstract void Handle(TEvent busMessage);
    }
}