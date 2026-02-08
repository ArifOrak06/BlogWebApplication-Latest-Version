using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Repository.Contexts.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogWebApplication.Repository.Repositories
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public AppUserRepository(AppDbContext appDbContext, UserManager<AppUser> userManager) : base(appDbContext)
        {
            _userManager = userManager;
        }

        public async Task<List<AppUser>> GetAllAppUsersWithArticlesAndImgAsync(bool trackChanges, Expression<Func<AppUser, bool>> predicate = null, params Expression<Func<AppUser, object>>[] includeProperties)
        {
            IQueryable<AppUser> query = _userManager.Users;
            query = !trackChanges ? query.AsNoTracking() : query;
            query = predicate is null ? query : query.Where(predicate);
            if(includeProperties.Any())
                foreach (var property in includeProperties)
                    query = query.Include(property);
            return await query.ToListAsync();

        }

        public async Task<AppUser> GetAppUserWithArticlesAndImgAsync(bool trackChanges, Expression<Func<AppUser, bool>> predicate = null, params Expression<Func<AppUser, object>>[] includeProperties)
        {
            IQueryable<AppUser> query = _userManager.Users;
            query = !trackChanges ? query.AsNoTracking().Where(predicate) : query.Where(predicate);
            if(includeProperties.Any()) 
                foreach (var property in includeProperties)
                    query = query.Include(property);
            return await query.SingleOrDefaultAsync();
        
        }
    }
}
