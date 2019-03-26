using Bus.Abstractions;

namespace TextRankCalc.BusMessage
{
    public class TextCreated: IBusMessage
    {
        public int DatabaseNumber {get; set;}
    }
}