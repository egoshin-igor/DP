namespace VowelConsRater.BusMessages.IntegrationEvents
{
    public class TextRankCalculated: IntegrationEvent.IntegrationEvent
    {
        public string ContextId { get; set; }
        public double Rank { get; set; }
    }
}