namespace BackendApi.IntegrationEvents
{
    public class TextCreated: IntegrationEvent.IntegrationEvent
    {
        public string TextId {get; set;}
    }
}