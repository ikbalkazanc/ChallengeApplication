using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Redis
{
    public interface IRedisService
    {
        void Connect();
        Task SaveUser(string email, string connectionId);
        Task RemoveUser(string email);
        Task<string> GetConnectionId(string email);
    }
}
