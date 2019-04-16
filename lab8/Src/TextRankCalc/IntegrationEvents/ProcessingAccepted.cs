namespace TextRankCalc.IntegrationEvents
{
    public class ProcessingAccepted : IntegrationEvent.IntegrationEvent
    {
        public string ContextId { get; set; }
        public bool Status { get; set; }
    }
}