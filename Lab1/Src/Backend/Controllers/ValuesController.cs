﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static readonly ConcurrentDictionary<string, string> _data = new ConcurrentDictionary<string, string>();

        [HttpGet("{id}")]
        public string Get(string id)
        {
            string value = null;
            _data.TryGetValue(id, out value);

            return value;
        }

        [HttpPost]
        public string Post([FromBody]string value)
        {
            var id = Guid.NewGuid().ToString();
            _data[id] = value;
            
            return id;
        }
    }
}
