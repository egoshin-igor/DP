using System;
using Bus.Abstractions;
using IntegrationEvent;
using KeyValueStorage.Abstractions;
using TextRankCalc.BusMessage;
using TextRankCalc.IntegrationEvents;

namespace TextRankCalc.IntegrationEventHandlers
{
    public class TextCreatedHandler : IntegrationEventHandler<IntegrationEvents.TextCreated>
    {
        IKeyValueStorage _storage;
        IBus _bus;

        public TextCreatedHandler(IKeyValueStorage keyValueStorage, IBus bus)
        {
            _storage = keyValueStorage;
            _bus = bus;
        }

        public override void Handle(IntegrationEvents.TextCreated @event)
        {
            Console.WriteLine($"Database: {_storage.Get(@event.TextId)}, {@event.TextId}");
            _storage.AddMessageToQueue(queueName: "VowelConsCounter", message: $"id:{@event.TextId}");
            _bus.Publish(
                busName: "VowelConsCounter",
                busMessage: new BusMessage.TextCreated {}
            );

            
        }
    }
}
