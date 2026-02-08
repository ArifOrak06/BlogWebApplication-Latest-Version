using BlogWebApplication.Core.Entities.Concrete;
using System.Linq.Expressions;

namespace BlogWebApplication.Core.Repositories
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
        Task<List<AppUser>> GetAllAppUsersWithArticlesAndImgAsync(bool trackChanges, Expression<Func<AppUser, bool>> predicate=null, params Expression<Func<AppUser, object>>[] includeProperties);
        Task<AppUser> GetAppUserWithArticlesAndImgAsync(bool trackChanges, Expression<Func<AppUser, bool>> predicate=null, params Expression<Func<AppUser, object>>[] includeProperties);
    }
}
