﻿using System;
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
            _storage.Set(id, databaseNumber.ToString());
            _eventBus.Publish(new TextCreated { ContextId = id });

            return id;
        }

        [HttpGet("text-processing-result")]
        public TextProcessingResultDto GetTextProcessingResult([FromQuery]string textId)
        {
            int? databaseNumber = int.Parse(_storage.Get(textId));
            if (databaseNumber == null)
            {
                throw new ApplicationException($"Database namber by text id not found. ContextId {textId}");
            }

            const int retryCount = 3;
            TextProcessingResultDto result =
                _storage.Get<TextProcessingResultDto>($"TextProcessingResult_{textId}", retryCount, databaseNumber.Value);

            return result;
        }

        [HttpGet("statistic")]
        public TextStatisticDto GetStatistic()
        {
            TextStatisticDto textStatistic = _storage.Get<TextStatisticDto>("Text_Statistic");

            return textStatistic;
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
    }
}
