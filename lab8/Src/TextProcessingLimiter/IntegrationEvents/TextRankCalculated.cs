namespace TextProcessingLimiter.IntegrationEvents
{
    public class TextRankCalculated: IntegrationEvent.IntegrationEvent
    {
        public double? Rank {get; set;}
    }
}