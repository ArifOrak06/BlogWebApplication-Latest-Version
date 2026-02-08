using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Repository.Contexts.EfCore;
using Microsoft.AspNetCore.Identity;

namespace BlogWebApplication.Repository.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IArticleRepository> _articleRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IImgRepository> _imgRepository;
        private readonly Lazy<IAppUserRepository> _appUserRepository;
     
        public RepositoryManager(AppDbContext context, UserManager<AppUser> userManager)
        {
            _articleRepository = new Lazy<IArticleRepository>(() => new ArticleRepository(context));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
            _imgRepository = new Lazy<IImgRepository>(() => new ImgRepository(context));
            _appUserRepository = new Lazy<IAppUserRepository>(() => new AppUserRepository(context,userManager));
           
        }
        public IArticleRepository ArticleRepository => _articleRepository.Value;

        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public IImgRepository ImgRepository => _imgRepository.Value;

        public IAppUserRepository AppUserRepository => _appUserRepository.Value;
    }
}
