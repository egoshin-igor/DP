namespace Bus.Abstractions
{
    public interface IBusMessageHandler<T> where T: IBusMessage
    {
        void Handle(T busMessage);
    }
}