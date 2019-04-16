using System;
using System.Collections.Generic;
using Bus.Abstractions;
using KeyValueStorage.Abstractions;
using VowelConsCounter.BusMessages;

namespace VowelConsCounter.BusMessageHandlers
{
    public class TextCreatedHandler : IBusMessageHandler<TextCreated>
    {
        IKeyValueStorage _keyValueStorage;
        IBus _bus;

        public TextCreatedHandler(IKeyValueStorage keyValueStorage, IBus bus)
        {
            _keyValueStorage = keyValueStorage;
            _bus = bus;
        }
        public void Handle(TextCreated busMessage)
        {
            const string vowelConsCounerQueueName = "VowelConsCounter";

            string job = _keyValueStorage.GetMessageFromQueue(vowelConsCounerQueueName);
            while (job != null)
            {
                string contextId = job.Split(':')[1];
                int databaseNumber = int.Parse(_keyValueStorage.Get(contextId));
                Console.WriteLine($"Database: {databaseNumber}, {contextId}");
                
                string text = _keyValueStorage.Get(contextId, retryCount: 1 , databaseNumber: databaseNumber);

                KeyValuePair<int, int> vowelConsCount = GetVowelConsCount(text);

                _keyValueStorage.AddMessageToQueue(
                    queueName: "VowelConsRater",
                    message: $"{contextId}:{vowelConsCount.Key}:{vowelConsCount.Value}"
                );

                _bus.Publish(
                    busName: "VowelConsRater",
                    busMessage: new VowelConsCounted {}
                );

                job = _keyValueStorage.GetMessageFromQueue(vowelConsCounerQueueName);
            }
        }

        private bool isVowel(char ch)
        {
            const string vowels = "aeiouyAEIOY";

            return vowels.IndexOf(ch) >= 0;
        }

        private bool isConsonant(char ch)
        {
            const string consonants = "bcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ";

            return consonants.IndexOf(ch) >= 0;
        }

        private KeyValuePair<int, int> GetVowelConsCount(string text)
        {
            int consonantsQuantity = 0;
            int vowelsQuantity = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (isConsonant(ch))
                {
                    consonantsQuantity++;
                }
                else if (isVowel(ch))
                {
                    vowelsQuantity++;
                }
            }

            return KeyValuePair.Create(vowelsQuantity, consonantsQuantity);
        }
    }
}