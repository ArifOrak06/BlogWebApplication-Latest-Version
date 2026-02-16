using BlogWebApplication.Core.Models.CategoryModels;
using BlogWebApplication.SharedLibrary.RRP;

namespace BlogWebApplication.Core.Services
{
    public interface ICategoryService
    {
        Task<CustomResponseModel<List<CategoryViewModel>>> GetAllActiveCategoriesWithArticlesAsync();
        Task<CustomResponseModel<List<CategoryViewModel>>> GetAllSoftDeletedCategoriesWithArticlesAsync();
        Task<CustomResponseModel<CategoryViewModel>> GetOneCategoryWithArticlesByCategoryIdAsync(Guid categoryId);
        Task<CustomResponseModel<CategoryCreateViewModel>> CreateOneCategoryAsync(CategoryCreateViewModel request);
        Task<CustomResponseModel<CategoryUpdateViewModel>> UpdateOneCategoryAsync(CategoryUpdateViewModel request);
        Task<CustomResponseModel<NoContentModel>> DeleteOneCategoryAsync(Guid categoryId);
        Task<CustomResponseModel<NoContentModel>> SoftDeleteOneCategoryAsync(Guid categoryId);
        Task<CustomResponseModel<NoContentModel>> RestoreOneCategoryAsync(Guid categoryId);

        }
}
