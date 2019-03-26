using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Dtos;
using BackendApi.IntegrationEvents;
using BackendApi.Types;
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
        private readonly RegionDatabaseNumbers _regionDatabaseNumbers;

        public TextController(IEventBus eventBus, IKeyValueStorage keyValueStorage, RegionDatabaseNumbers regionDatabaseNumbers)
        {
            _eventBus = eventBus;
            _storage = keyValueStorage;
            _regionDatabaseNumbers = regionDatabaseNumbers;
        }

        [HttpPost]
        public string Post([FromBody] UploadDto uploadDto)
        {
            string id = Guid.NewGuid().ToString();
            int databaseNumber = GetDatabaseNumberByRegionType(uploadDto.RegionType);
            _storage.Set(id, uploadDto.Text, databaseNumber);
            _eventBus.Publish(new TextCreated { TextId = id, DatabaseNumber = databaseNumber });

            return id;
        }

        [HttpGet("rank")]
        public double? GetRank([FromQuery]string textId)
        {
            int? databaseNumber = GetDatabaseNumberByKey(textId);
            if (databaseNumber == null)
            {
                return null;
            }
            const int retryCount = 3;
            string rankString = _storage.Get($"Rank_{textId}", retryCount, databaseNumber.Value);
            
            double rank;
            if (rankString == null || !double.TryParse(rankString, out rank))
            {
                return null;
            }

            return rank;
        }

        private int GetDatabaseNumberByRegionType(RegionType regionType)
        {
            switch (regionType)
            {
                case RegionType.Ru:
                    return _regionDatabaseNumbers.Ru;
                case RegionType.Eu:
                    return _regionDatabaseNumbers.Eu;
                case RegionType.Usa:
                    return _regionDatabaseNumbers.Usa;
                default:
                    throw new ApplicationException("Undefined region");
            }
        }

        private int? GetDatabaseNumberByKey(string key) 
        {
            foreach(int i in _regionDatabaseNumbers.GetNumbers())
            {
                if (_storage.Get(key, retryCount: 1, databaseNumber: i) != null)
                {
                    return i;
                }
            }

            return null;
        }
    }
}
