using Challenge.Core.Repositories;
using Challenge.Core.Settings;
using Challenge.Data.Context;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MongoDbContext _context;
        protected IMongoCollection<TEntity> _collection;
        public Repository(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<TEntity>();
            

        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                //Logging
            }
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = new List<TEntity>();
            try
            {
                result = await _collection.AsQueryable<TEntity>().ToListAsync();
            }
            catch(Exception ex)
            {
                //Logging
            }
            return result;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            TEntity result;
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            result = await _collection.Find(filter).FirstOrDefaultAsync();
            return result;
        } 

        public async Task Remove(string id)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("_id", id);
                await _collection.FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                //Logging
            }
        }

        public async Task Update(TEntity entity,string id)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("_id", id);
                await _collection.ReplaceOneAsync(filter, entity);
            }
            catch (Exception ex)
            {
                //Logging
            }

        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var result = new List<TEntity>();
            try
            {
                result = await _collection.Find(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                //Logging
            }
            return result;
        }
    }
}
