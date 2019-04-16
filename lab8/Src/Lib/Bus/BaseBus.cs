using System;
using Bus.Abstractions;
using KeyValueStorage.Abstractions;
using Newtonsoft.Json;

namespace Bus
{
    public class BaseBus : IBus
    {
        private const string MessagePrefix = "Message_";

        private readonly IKeyValueStorage _keyValueStorage;

        public BaseBus(IKeyValueStorage keyValueStorage)
        {
            _keyValueStorage = keyValueStorage;
        }
        public void Publish<TMessage>(string busName, TMessage busMessage) where TMessage : IBusMessage
        {
            string busMessageBody = JsonConvert.SerializeObject(busMessage);
            Type busMessageType = typeof(TMessage);
            string fullEvent = $"{MessagePrefix}{busMessageType.Name}{busMessageBody}";

            _keyValueStorage.Publish(busName, fullEvent);
        }

        public void Subscribe<TMessage, TMessageHandler>(string busName)
            where TMessage : IBusMessage
            where TMessageHandler : IBusMessageHandler<TMessage>, new()
        {
            Subscribe<TMessage, TMessageHandler>(busName, new TMessageHandler());
        }

        public void Subscribe<TMessage, TMessageHandler>(string busName, TMessageHandler messageHandler)
            where TMessage : IBusMessage
            where TMessageHandler : IBusMessageHandler<TMessage>
        {
            Subscribe<TMessage>(busName, (busMessage) => messageHandler.Handle(busMessage));
        }

        public void Subscribe<TMessage>(string busName, Action<TMessage> action) where TMessage : IBusMessage
        {
            Type busMessageType = typeof(TMessage);
            string observableMessageName = MessagePrefix + busMessageType.Name;

            _keyValueStorage.Subscribe(busName, (message) =>
            {
                string messageNameFromBus = message.Substring(0, observableMessageName.Length);
                if (messageNameFromBus != observableMessageName)
                {
                    return;
                }

                string busMessageBody = message.Substring(observableMessageName.Length);
                TMessage busMessage = JsonConvert.DeserializeObject<TMessage>(busMessageBody);
                if (action != null)
                {
                    action(busMessage);
                }
            });
        }
    }
}
