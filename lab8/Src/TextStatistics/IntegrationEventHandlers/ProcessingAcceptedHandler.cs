using System;
using IntegrationEvent;
using KeyValueStorage.Abstractions;
using TextStatistics.Dtos;
using TextStatistics.IntegrationEvents;

namespace TextStatistics.IntegrationEventHandlers
{
    public class ProcessingAcceptedHandler : IntegrationEventHandler<ProcessingAccepted>
    {
        private static int _failureTextCount = 0;

        private IKeyValueStorage _storage;

        public ProcessingAcceptedHandler(IKeyValueStorage keyValueStorage)
        {
            _storage = keyValueStorage;
        }

        public override void Handle(ProcessingAccepted @event)
        {
            if (@event.Status)
            {
                return;
            }

            _failureTextCount++;
            Console.WriteLine($"Failure text count: {_failureTextCount}");
            UpdateTextStatistic();
        }

        private void UpdateTextStatistic()
        {
            TextStatistic textStatistic = _storage.Get<TextStatistic>("Text_Statistic");
            if (textStatistic == null)
            {
                textStatistic = new TextStatistic();
            }
            textStatistic.FailureTextCount = _failureTextCount;

            _storage.Set("Text_Statistic", textStatistic);
        }
    }
}