using System;

namespace Bus.Abstractions
{
    public interface IBus
    {
        void Subscribe<TMessage, TMessageHandler>(string busName)
            where TMessage : IBusMessage
            where TMessageHandler : IBusMessageHandler<TMessage>, new();

        void Subscribe<TMessage>(string busName, Action<TMessage> action)
            where TMessage : IBusMessage;

        void Subscribe<TMessage, TMessageHandler>(string busName, TMessageHandler messageHandler)
            where TMessage : IBusMessage
            where TMessageHandler : IBusMessageHandler<TMessage>;

        void Publish<TMessage>(string busName, TMessage busMessage)
            where TMessage : IBusMessage;
    }
}
