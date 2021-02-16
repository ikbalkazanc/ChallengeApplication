using Challenge.Core.Repositories;
using Challenge.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Services
{
    public class Service<TEntity, TDto> : IService<TEntity, TDto> where TEntity : class where TDto : class
    {
        public readonly IRepository<TEntity> _repository;
        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task<TDto> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.AddAsync(newEntity);
            return entity;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var results = ObjectMapper.Mapper.Map<List<TDto>>(await _repository.GetAllAsync());
            return results;
        }

        public async Task<TDto> GetByIdAsync(string id)
        {
            var result = await _repository.GetByIdAsync(id);
            var dto = ObjectMapper.Mapper.Map<TDto>(result);
            return dto;
        }

        public async Task Remove(string id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(TDto entity, string id)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.Update(newEntity,id);
        }

        public async Task<IEnumerable<TDto>> Where(Expression<Func<TDto, bool>> predicate)
        {
            var newPredicate = ObjectMapper.Mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            var result = await _repository.Where(newPredicate);
            var newResult = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(result);
            return newResult;
        }
    }
}
