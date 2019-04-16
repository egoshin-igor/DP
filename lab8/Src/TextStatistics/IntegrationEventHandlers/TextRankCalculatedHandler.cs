using System;
using IntegrationEvent;
using KeyValueStorage.Abstractions;
using TextStatistics.Dtos;
using TextStatistics.IntegrationEvents;

namespace TextStatistics.IntegrationEventHandlers
{
    public class TextRankCalculatedHandler : IntegrationEventHandler<TextRankCalculated>
    {
        private static int _textNum = 0;
        private static int _highRankPart = 0;
        private static double _avgRank = 0;
        private static double _rankSum = 0;

        private IKeyValueStorage _storage;

        public TextRankCalculatedHandler(IKeyValueStorage keyValueStorage)
        {
            _storage = keyValueStorage;
        }

        public override void Handle(TextRankCalculated @event)
        {
            _textNum++;
            if (@event.Rank != null) 
            {
                _rankSum += @event.Rank.Value;
            }

            if (@event.Rank >= 0.5)
            {
                _highRankPart++;
            }

            _avgRank = _rankSum / _textNum;

            Console.WriteLine($"Text number: {_textNum}, high rank part: {_highRankPart}, average rank: {Math.Round(_avgRank, 2)}");
            UpdateTextStatistic();
        }

        private void UpdateTextStatistic()
        {
            TextStatistic textStatistic = _storage.Get<TextStatistic>("Text_Statistic");
            if (textStatistic == null)
            {
                textStatistic = new TextStatistic();
            }
            textStatistic.TextNum = _textNum;
            textStatistic.AvgRank = _avgRank;
            textStatistic.HighRankPart = _highRankPart;

            _storage.Set("Text_Statistic", textStatistic);
        }
    }
}