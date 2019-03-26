using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.IntegrationEvents;
using IntegrationEvent;
using KeyValueStorage.Abstractions;
using Lib.Http.Client;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly IKeyValueStorage _storage;

        public TextController(IEventBus eventBus, IKeyValueStorage keyValueStorage)
        {
            _eventBus = eventBus;
            _storage = keyValueStorage;
        }

        [HttpPost]
        public string Post([FromBody]string value)
        {
            string id = Guid.NewGuid().ToString();
            _storage.Set(id, value);
            _eventBus.Publish(new TextCreated { TextId = id });

            return id;
        }

        [HttpGet("rank")]
        public double? GetRank([FromQuery]string textId)
        {
            const int retryCount = 3;
            string rankString = _storage.Get($"Rank_{textId}", retryCount);
            
            double rank;
            if (rankString == null || !double.TryParse(rankString, out rank))
            {
                return null;
            }

            return rank;
        }
    }
}
