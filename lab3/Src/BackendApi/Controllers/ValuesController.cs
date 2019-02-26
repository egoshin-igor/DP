using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.IntegrationEvents;
using IntegrationEvent;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ConnectionMultiplexer _redis;

        public ValuesController(IEventBus eventBus, ConnectionMultiplexer redis)
        {
            _eventBus = eventBus;
            _redis = redis;
        }

        [HttpPost]
        public string Post([FromBody]string value)
        {
            var id = Guid.NewGuid().ToString();
            IDatabase redisDb = _redis.GetDatabase();
            redisDb.StringSet(id, value);
            _eventBus.Publish(new TextCreated { TextId = id });

            return id;
        }
    }
}
