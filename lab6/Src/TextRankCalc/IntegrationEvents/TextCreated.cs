namespace TextRankCalc.IntegrationEvents
{
    public class TextCreated : IntegrationEvent.IntegrationEvent
    {
        public string TextId { get; set; }
        public int DatabaseNumber { get; set; }
    }
}