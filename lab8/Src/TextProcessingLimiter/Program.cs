using System;
using System.IO;
using System.Threading.Tasks;
using Bus;
using Bus.Abstractions;
using IntegrationEvent;
using KeyValueStorage;
using KeyValueStorage.Abstractions;
using Microsoft.Extensions.Configuration;
using TextProcessingLimiter.IntegrationEvents;

namespace TextProcessingLimiter
{
    class Program
    {
        static void Main(string[] args)
        {
            TextProcessingSettings textProcessingSettings = GetTextProcessingSettingsFromAppConfig();
            IEventBus eventBus = GetInitedEventBus();
            int permissionsCount = textProcessingSettings.Limit;

            eventBus.Subscribe<TimeToUpdateTextPermissionsCountHasCome>((@event) =>
            {
                permissionsCount = textProcessingSettings.Limit;
                Task.Run(async () =>
                {
                    await Task.Delay(millisecondsDelay: textProcessingSettings.UpdatePermissionsCountInSeconds * 1000);
                    eventBus.Publish(new TimeToUpdateTextPermissionsCountHasCome());
                });
            });
            eventBus.Publish(new TimeToUpdateTextPermissionsCountHasCome());

            eventBus.Subscribe(delegate (TextCreated @event) 
            {
                if (permissionsCount <= 0)
                {
                    eventBus.Publish(new ProcessingAccepted{ ContextId = @event.ContextId, Status = false });
                    return;
                }

                permissionsCount--;
                eventBus.Publish(new ProcessingAccepted { ContextId = @event.ContextId, Status = true });
            });
            
            eventBus.Subscribe(delegate(TextRankCalculated @event)
            {
                bool isFailureText = @event.Rank == null || @event.Rank <= 0.5;
                if (isFailureText) 
                {
                    permissionsCount++;
                }
            });
            
            Console.ReadLine();
        }

        private static EventBus GetInitedEventBus()
        {
            IKeyValueStorage keyValueStorage = new RedisStore();
            IBus bus = new BaseBus(keyValueStorage);
            
            return new EventBus(bus);
        }

        private static TextProcessingSettings GetTextProcessingSettingsFromAppConfig()
        {
            string settingsPath = Directory.GetCurrentDirectory();
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile($"{settingsPath}/appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config.GetSection("TextProcessingSettings").Get<TextProcessingSettings>();
        }
    }
}
