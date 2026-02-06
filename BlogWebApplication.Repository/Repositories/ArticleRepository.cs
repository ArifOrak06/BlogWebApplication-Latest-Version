using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Repository.Contexts.EfCore;

namespace BlogWebApplication.Repository.Repositories
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
