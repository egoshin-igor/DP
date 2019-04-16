using System;
using IntegrationEvent;
using KeyValueStorage;
using KeyValueStorage.Abstractions;
using StackExchange.Redis;
using TextListener.IntegrationEvents;

namespace TextListener.IntegrationEventHandlers
{
    public class TextCreatedHandler : IntegrationEventHandler<TextCreated>
    {
        IKeyValueStorage _storage;

        public TextCreatedHandler(IKeyValueStorage keyValueStorage)
        {
            _storage = keyValueStorage;
        }

        public override void Handle(TextCreated @event)
        {
            int databaseNumber = int.Parse(_storage.Get(@event.ContextId));
            Console.WriteLine($"Database: {databaseNumber}, TextId: {@event.ContextId}, " + 
                $"Text: {_storage.Get(@event.ContextId, retryCount: 1, databaseNumber: databaseNumber)}");
        }
    }
}