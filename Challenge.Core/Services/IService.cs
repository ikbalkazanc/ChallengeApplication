using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Services
{
    public interface IService<TEntity,TDto> where TEntity : class where TDto : class
    {
        Task<TDto> GetByIdAsync(string id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<IEnumerable<TDto>> Where(Expression<Func<TDto, bool>> predicate);
        Task<TDto> AddAsync(TDto entity);
        Task Remove(string id);
        Task Update(TDto entity, string id);
    }
}
