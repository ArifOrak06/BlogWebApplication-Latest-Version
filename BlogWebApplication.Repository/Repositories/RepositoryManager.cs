using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Repository.Contexts.EfCore;

namespace BlogWebApplication.Repository.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IArticleRepository> _articleRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IImgRepository> _imgRepository;

        public RepositoryManager(AppDbContext context)
        {
            _articleRepository = new Lazy<IArticleRepository>(() => new ArticleRepository(context));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
            _imgRepository = new Lazy<IImgRepository>(() => new ImgRepository(context));
        }
        public IArticleRepository ArticleRepository => _articleRepository.Value;

        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public IImgRepository ImgRepository => _imgRepository.Value;
    }
}
