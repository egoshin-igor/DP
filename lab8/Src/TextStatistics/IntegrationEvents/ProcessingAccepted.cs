namespace TextStatistics.IntegrationEvents
{
    public class ProcessingAccepted : IntegrationEvent.IntegrationEvent
    {
        public bool Status { get; set; }
    }
}