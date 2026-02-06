using BlogWebApplication.Core.Entities.Abstract;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Repository.Contexts.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogWebApplication.Repository.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appDbContext;

        protected RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity) => _appDbContext.Set<T>().Remove(entity);

        public void DeleteRange(List<T> entities) => _appDbContext.Set<T>().RemoveRange(entities);

        public async Task<List<T>> GetAllAsync(bool trackChanges, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            query = (!trackChanges && predicate is null) ? query.AsNoTracking() : query.Where(predicate);
            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);

            return await query.ToListAsync();
        }
            
       

        public IQueryable<T> GetByFilter(bool trackChanges, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _appDbContext.Set<T>();
            query = !trackChanges ? query.AsNoTracking().Where(predicate) : query.Where(predicate);
            if (includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);
            return query;
        }

        public void Update(T entity) => _appDbContext.Set<T>().Update(entity);
    }
}
