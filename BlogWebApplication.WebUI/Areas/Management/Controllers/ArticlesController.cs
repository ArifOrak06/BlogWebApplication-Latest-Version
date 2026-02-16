using BlogWebApplication.Core.Models.ArticleModels;
using BlogWebApplication.Core.Models.CategoryModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using BlogWebApplication.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.WebUI.Areas.Management.Controllers
{
    [Authorize]
    [Area("Management")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            CustomResponseModel<List<ArticleViewModel>>? result = await _articleService.GetAllActivesAndNonDeletedArticlesWithCategoryAndAppUserAsync();
            if (result.ResponseType == ResponseType.Error)
            {
                ViewData["StatusMessage"] = result.Errors!.First();
                return NotFound();
            }
            ViewData["StatusMessage"] = result.isSuccessMessage;
            return View(result.Data);

        }

        public async Task<IActionResult> AddArticle()
        {
            List<CategoryViewModel>? categorieModels = (await _categoryService.GetAllActiveCategoriesWithArticlesAsync()).Data;
            return View(new ArticleCreateViewModel
            {
                Categories = categorieModels
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddArticle(ArticleCreateViewModel request)
        {
            CustomResponseModel<ArticleCreateViewModel>? result = await _articleService.CreateOneArticleAsync(request);
            if(result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View();
            }
            if(result.ResponseType == ResponseType.Error)
            {
                ViewData["StatusMessage"] = result.Errors!.First();
                return View();
            }
            TempData["StatusMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
