using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Repositories.IRepositories;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace demo_tt.Repositories
{
    public class CacheRepository : ICacheRepository
    {

        private IDatabase _db;

        public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _db = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetCacheData<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public async Task<object> RemoveData(string key)
        {
            bool isExistKey = await _db.KeyExistsAsync(key);
            if (isExistKey) return await _db.KeyDeleteAsync(key);
            return false;
        }

        public async Task<bool> SetCacheData<T>(string key, T value, DateTimeOffset expiration)
        {
            TimeSpan expireTime = expiration.DateTime.Subtract(DateTime.Now);
            string json = JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            var result = await _db.StringSetAsync(key, json, expireTime);
            return result;
        }
    }
}