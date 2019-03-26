using Bus.Abstractions;

namespace VowelConsCounter.BusMessages
{
    public class VowelConsCounted: IBusMessage
    {
        public int DatabaseNumber {get; set;}
    }
}