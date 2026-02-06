using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Repository.Contexts.EfCore;

namespace BlogWebApplication.Repository.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
