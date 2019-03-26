using Bus.Abstractions;
using KeyValueStorage.Abstractions;

namespace VowelConsRater.BusMessageHandlers
{
    public class VowelConsCountedHandler : IBusMessageHandler<VowelConsCounted>
    {
        private readonly IKeyValueStorage _keyValueStorage;

        public VowelConsCountedHandler(IKeyValueStorage keyValueStorage)
        {
            _keyValueStorage = keyValueStorage;
        }

        public void Handle(VowelConsCounted busMessage)
        {
            const string queueName = "VowelConsRater";

            string job = _keyValueStorage.GetMessageFromQueue(queueName);
            while (job != null)
            {
                string[] jobParts = job.Split(':');

                string contextId = jobParts[0];
                int vowelsCount = int.Parse(jobParts[1]);
                int consCount = int.Parse(jobParts[2]);

                double rank = consCount != 0 ? (double)vowelsCount / consCount : double.PositiveInfinity;
                _keyValueStorage.Set($"Rank_{contextId}", rank.ToString());

                job = _keyValueStorage.GetMessageFromQueue(queueName);
            }
        }
    }
}