using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Challenge.Core.Redis
{
    public interface IRedisRepository
    {
        void Connect();
        IDatabase GetDatabase(int db = 0);
        Task Add(string key, string value, TimeSpan time, int db = 0);
        Task Add(string key, string value, int db = 0);
        Task Remove(string key, int db = 0);
        Task<string> GetValue(string key, int db = 0);
    }
}
