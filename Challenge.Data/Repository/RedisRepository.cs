using Challenge.Core.Redis;
using Challenge.Core.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Data.Repository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly string connStr;
        private ConnectionMultiplexer _redis;
        public IDatabase db { get; set; }
        public RedisRepository(IOptions<RedisSettings> connString)
        {
            connStr = connString.Value.ConnectionString;
        }

        public async Task Add(string key, string value,TimeSpan time,  int db = 0)
        {
            var database = _redis.GetDatabase(db);
            await database.StringSetAsync(key,value,time);
        }
        public async Task Add(string key, string value, int db = 0)
        {
            await this.Add(key, value, TimeSpan.FromDays(1), db);
        }

        public async Task Remove(string key, int db = 0)
        {
            var database = _redis.GetDatabase(db);
            await database.KeyDeleteAsync(key);
        }

        public async Task<string> GetValue(string key, int db = 0)
        {
            var database = _redis.GetDatabase(db);
            var value = await database.StringGetAsync(key);
            return value;
        }

        public IDatabase GetDatabase(int db = 0)
        {
            return _redis.GetDatabase();
        }

        public void Connect()
        {
            _redis = ConnectionMultiplexer.Connect(connStr);
        }
    }
}
