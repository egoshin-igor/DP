using Bus.Abstractions;

namespace VowelConsCounter.BusMessages
{
    public class TextCreated: IBusMessage
    {
        public int DatabaseNumber {get; set;}
    }
}