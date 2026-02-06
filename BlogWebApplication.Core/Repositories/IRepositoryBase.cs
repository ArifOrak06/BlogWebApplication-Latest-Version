using System.Linq.Expressions;

namespace BlogWebApplication.Core.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAllAsync(bool trackChanges,Expression<Func<T, bool>> predicate=null,params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetByFilter(bool trackChanges, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
    }
}
