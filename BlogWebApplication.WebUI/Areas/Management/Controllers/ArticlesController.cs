using BlogWebApplication.Core.Models.ArticleModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;


        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
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
    }
}
