using BlogWebApplication.Core.Models.CategoryModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.RRP;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.WebUI.ViewComponents
{
    public class GetAllCategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CustomResponseModel<List<CategoryViewModel>> result = await _categoryService.GetAllActiveCategoriesWithArticlesAsync();
            return View(result.Data);
        }
    }
}
