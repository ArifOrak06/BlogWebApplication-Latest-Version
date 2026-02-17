using AutoMapper;
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
        private readonly IMapper _mapper;

        public ArticlesController(IArticleService articleService, ICategoryService categoryService, IMapper mapper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
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
            if (result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View();
            }
            if (result.ResponseType == ResponseType.Error)
            {
                ViewData["StatusMessage"] = result.Errors!.First();
                return View();
            }
            TempData["StatusMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(HomeController.Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditArticle(Guid articleId)
        {
            CustomResponseModel<ArticleViewModel> result = await _articleService.GetOneActiveArticleWithCategoryAndAppUserByArticleIdAsync(articleId);
            if (result.ResponseType == ResponseType.NotFound)
            {
                TempData["StatusMessage"] = result.Errors!.First();
                return NotFound();

            }
            ArticleUpdateViewModel model = _mapper.Map<ArticleUpdateViewModel>(result.Data);
            model.Categories = (await _categoryService.GetAllActiveCategoriesWithArticlesAsync()).Data;
            return View(model);
        }
        public async Task<IActionResult> EditArticle(ArticleUpdateViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            CustomResponseModel<ArticleUpdateViewModel>? result = await _articleService.UpdateOneArticleAsync(request);
            if(result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View(result.Data);
            }
            if(result.ResponseType == ResponseType.NotFound)
            {
                ViewData["StatusMessage"] = result.Errors!.First();
                return NotFound();
            }
            TempData["StatusMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> SoftDeleteArticle(Guid articleId)
        {
            CustomResponseModel<NoContentModel>? result = await _articleService.SoftDeleteOneArticleAsync(articleId);
            if (result.ResponseType == ResponseType.NotFound)
            {
                ViewData["StatusMessage"] = result.Errors!.First();
                return NotFound();
            }
            TempData["StatusMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}
