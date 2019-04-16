namespace TextProcessingLimiter.IntegrationEvents
{
    public class TextCreated: IntegrationEvent.IntegrationEvent
    {
        public string ContextId {get; set;}
    }
}