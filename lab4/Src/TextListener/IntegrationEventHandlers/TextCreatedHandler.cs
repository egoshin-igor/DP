using System;
using IntegrationEvent;
using KeyValueStorage;
using StackExchange.Redis;
using TextListener.IntegrationEvents;

namespace TextListener.IntegrationEventHandlers
{
    public class TextCreatedHandler : IIntegrationEventHandler<TextCreated>
    {
        IKeyValueStorage _storage;

        public TextCreatedHandler(IKeyValueStorage keyValueStorage)
        {
            _storage = keyValueStorage;
        }

        public void Handle(TextCreated @event)
        {
            Console.WriteLine($"TextId: {@event.TextId}, Text: {_storage.Get(@event.TextId)}");
        }
    }
}