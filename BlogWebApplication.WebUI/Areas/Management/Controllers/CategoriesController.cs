using AutoMapper;
using BlogWebApplication.Core.Models.CategoryModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using BlogWebApplication.WebUI.Consts;
using BlogWebApplication.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> Index()
        {
            CustomResponseModel<List<CategoryViewModel>>? result = await _categoryService.GetAllActiveCategoriesWithArticlesAsync();
            if(result.ResponseType == ResponseType.NotFound)
                return NotFound();  
            return View(result.Data);
        }
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> AddCategory(CategoryCreateViewModel request)
        {
            CustomResponseModel<CategoryCreateViewModel>? result = await _categoryService.CreateOneCategoryAsync(request);
            if(result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View(request);
            }
            TempData["StatusMessage"] = result.isSuccessMessage;    
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> EditCategory(Guid categoryId)
        {
            CustomResponseModel<CategoryViewModel>? result = await _categoryService.GetOneCategoryWithArticlesByCategoryIdAsync(categoryId);
            if(result.ResponseType == ResponseType.NotFound)
                return NotFound();
            return View(_mapper.Map<CategoryUpdateViewModel>(result.Data));
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.SuperAdmin},{RoleConsts.Admin}")]
        public async Task<IActionResult> EditCategory(CategoryUpdateViewModel request)
        {
            CustomResponseModel<CategoryUpdateViewModel>? result = await _categoryService.UpdateOneCategoryAsync(request);
            if(result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View(result.Data);
            }
            if(result.ResponseType == ResponseType.NotFound)
                return NotFound();
            TempData["StatusMessage"] = result.isSuccessMessage;    
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = $"{RoleConsts.SuperAdmin}")]
        public async Task<IActionResult> SoftDeleteCategory(Guid categoryId)
        {
            CustomResponseModel<NoContentModel> result = await _categoryService.SoftDeleteOneCategoryAsync(categoryId);
            if(result.ResponseType == ResponseType.NotFound)
                return NotFound();
            TempData["StatusMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}
