using System;
using IntegrationEvent;
using KeyValueStorage;
using TextRankCalc.IntegrationEvents;

namespace TextRankCalc.IntegrationEventHandlers
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
            double rank = TextRancCalc.Calculate(_storage.Get(@event.TextId));
            _storage.Set($"Rank_{@event.TextId}", rank.ToString());
        }
    }
}