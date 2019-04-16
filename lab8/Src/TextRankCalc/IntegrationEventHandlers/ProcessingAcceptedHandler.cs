using System;
using Bus.Abstractions;
using IntegrationEvent;
using KeyValueStorage.Abstractions;
using TextRankCalc.BusMessage;
using TextRankCalc.Dtos;
using TextRankCalc.IntegrationEvents;

namespace TextRankCalc.IntegrationEventHandlers
{
    public class ProcessingAcceptedHandler : IntegrationEventHandler<ProcessingAccepted>
    {
        IKeyValueStorage _storage;
        IBus _bus;

        public ProcessingAcceptedHandler(IKeyValueStorage keyValueStorage, IBus bus)
        {
            _storage = keyValueStorage;
            _bus = bus;
        }

        public override void Handle(ProcessingAccepted @event)
        {
            if (!@event.Status)
            {
                int databaseNumber = int.Parse(_storage.Get(@event.ContextId));
                var textProcessingResultDto = new TextProcessingResultDto { IsTextProcessingAllowed = false };
                _storage.Set($"TextProcessingResult_{@event.ContextId}", textProcessingResultDto, databaseNumber);
                return;
            }

            Console.WriteLine($"Database: {_storage.Get(@event.ContextId)}, {@event.ContextId}");
            _storage.AddMessageToQueue(queueName: "VowelConsCounter", message: $"id:{@event.ContextId}");
            _bus.Publish(
                busName: "VowelConsCounter",
                busMessage: new BusMessage.TextCreated {}
            );
        }
    }
}
