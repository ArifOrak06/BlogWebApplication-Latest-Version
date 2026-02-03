namespace BlogWebApplication.Core.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetByIdAsync(bool trackChanges,Guid id);
        Task<List<T>> GetAllAsync(bool trackChanges,Predicate<Func<T, bool>> predicate=null);
        IQueryable<T> GetByFilter(bool trackChanges, Predicate<Func<T, bool>> predicate, params Predicate<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
    }
}
