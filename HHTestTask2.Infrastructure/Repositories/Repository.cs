using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Repositories;
using HHTestTask2.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _entities;

        public Repository(DbSet<TEntity> entities) 
        { 
            _entities = entities; 
        }

        public async Task AddAsync(TEntity entity) 
        { 
            await _entities.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity) 
        { 
            await Task.Run(() => 
            { 
                _entities.Update(entity); 
            }); 
        }

        public async Task<TEntity> GetByIdAsync(int id, IEnumerable<string> entitiesToInclude) 
        {
            return await _entities.Include(entitiesToInclude).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> predicates, IEnumerable<string> entitiesToInclude) 
        {
            return await _entities.Include(entitiesToInclude).Filter(predicates).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int limit, IEnumerable<Expression<Func<TEntity, bool>>> predicates, IEnumerable<string> entitiesToInclude) 
        {

            return await _entities.Include(entitiesToInclude.ElementAt(0)).Filter(predicates).Skip(skip).Take(limit).ToListAsync();
        }

        public void Delete(TEntity entity) 
        { 
            _entities.Remove(entity); 
        }

        public async Task DeleteAsync(TEntity entity) 
        { 
            await Task.Run(() => 
            { 
                Delete(entity); 
            }); 
        }

        public async virtual Task<int> Count(IEnumerable<Expression<Func<TEntity, bool>>> predicates = null, IEnumerable<string> entitiesToInclude = null) 
        { 
            return await _entities.Include(entitiesToInclude).Filter(predicates).CountAsync(); 
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> entitiesToInclude = null) 
        { 
            return await _entities.Include(entitiesToInclude).AnyAsync(predicate); 
        }
    }
}
