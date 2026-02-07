using BlogWebApplication.Core.Models.ArticleModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.RRP;

namespace BlogWebApplication.Service.Services
{
    public class ArticleService : IArticleService
    {
        public Task<CustomResponseModel<ArticleCreateViewModel>> CreateOneArticleAsync(ArticleCreateViewModel articleCreateViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesAndNonDeletedArticlesWithCategoryAndAppUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithAppUserByCategoryIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithCategoryByAppUserIdAsync(Guid appUserId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllDeletedArticlesWithCategoryAndAppUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<ArticleViewModel>> GetOneActiveArticleWithCategoryAndAppUserByArticleIdAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<NoContentModel>> HardDeleteOneArticleAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<NoContentModel>> RestoreOneArticleAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<NoContentModel>> SoftDeleteOneArticleAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<ArticleUpdateViewModel>> UpdateOneArticleAsync(ArticleUpdateViewModel articleCreateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
