using Challenge.Core.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Data.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;
        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _db = client.GetDatabase(settings.Value.Database);
            
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _db.GetCollection<TEntity>("ChallengeUser");
        }
        public IMongoDatabase GetDatabase()
        {
            return _db;
        }
    }
}
