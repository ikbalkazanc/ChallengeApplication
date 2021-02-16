using Challenge.Core.Redis;
using System.Threading.Tasks;

namespace Challenge.Service.Services
{
    public class RedisService : IRedisService
    {
        private readonly IRedisRepository _redisRepository;
        public RedisService(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public void Connect()
        {
            _redisRepository.Connect();
        }

        public async Task<string> GetConnectionId(string connectionId)
        {
            return await _redisRepository.GetValue(connectionId);
        }

        public async Task RemoveUser(string connectionId)
        {
            await _redisRepository.Remove(connectionId);
        }

        public async Task SaveUser(string connectionId, string email)
        {
            await _redisRepository.Add(connectionId, email);
        }
    }
}
