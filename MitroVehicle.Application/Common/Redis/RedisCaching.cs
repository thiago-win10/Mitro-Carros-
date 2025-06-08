using Microsoft.Extensions.Caching.Distributed;
using MitroVehicle.Application.Common.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MitroVehicle.Application.Common.Redis.RedisCaching;

namespace MitroVehicle.Application.Common.Redis
{
    public class RedisCaching : IRedisCaching
    {
        private readonly IDatabase _database;
        
        public RedisCaching(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var json = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, json, expiration);
        }

    }
}
