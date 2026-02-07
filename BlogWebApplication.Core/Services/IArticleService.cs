using BlogWebApplication.Core.Models.ArticleModels;
using BlogWebApplication.SharedLibrary.RRP;

namespace BlogWebApplication.Core.Services
{
    public interface IArticleService
    {
        Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesAndNonDeletedArticlesWithCategoryAndAppUserAsync();
        Task<CustomResponseModel<List<ArticleViewModel>>> GetAllDeletedArticlesWithCategoryAndAppUserAsync();
        Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithCategoryByAppUserIdAsync(Guid appUserId);
        Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithAppUserByCategoryIdAsync(Guid categoryId);
        Task<CustomResponseModel<ArticleViewModel>> GetOneActiveArticleWithCategoryAndAppUserByArticleIdAsync(Guid articleId);
        Task<CustomResponseModel<ArticleCreateViewModel>> CreateOneArticleAsync(ArticleCreateViewModel articleCreateViewModel);
        Task<CustomResponseModel<ArticleUpdateViewModel>> UpdateOneArticleAsync(ArticleUpdateViewModel articleCreateViewModel);
        Task<CustomResponseModel<NoContentModel>> SoftDeleteOneArticleAsync(Guid articleId);
        Task<CustomResponseModel<NoContentModel>> RestoreOneArticleAsync(Guid articleId);
        Task<CustomResponseModel<NoContentModel>> HardDeleteOneArticleAsync(Guid articleId);

        

        
    }
}
