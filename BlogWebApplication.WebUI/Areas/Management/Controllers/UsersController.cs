using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using BlogWebApplication.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]

    public class UsersController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;


        public UsersController(IAppUserService appUserService, UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            CustomResponseModel<List<AppUserViewModel>> result = await _appUserService.GetAllAppUsersWithArticlesAndImgAsync();
            if (result.ResponseType == ResponseType.NotFound)
                return NotFound();

            return View(result.Data);
        }
        public IActionResult AddUser()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AppUserSignUpViewModel request)
        {
            var result = await _appUserService.CreateOneAppUserAsync(request);
            if(result.ResponseType == ResponseType.IdentityError)
            {
                ModelState.AddModelStateIdentityErrorList(result.IdentityErrors);
                return View(result.Data);
            }
            if(result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors);
                return View();
            }
            TempData["StatusMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EditUser(Guid appUserId)
        {
            CustomResponseModel<AppUserViewModel> result = await _appUserService.GetAppUserWithArticlesAndImgByUserIdAsync(appUserId);
            if(result.ResponseType == ResponseType.NotFound)
                return NotFound();
            return View(_mapper.Map<AppUserEditViewModel>(result.Data));
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(AppUserEditViewModel request)
        {
            CustomResponseModel<AppUserEditViewModel> result = await _appUserService.UpdateOneAppUserAsync(request);
            if(result.ResponseType == ResponseType.IdentityError)
            {
                ModelState.AddModelStateIdentityErrorList(result.IdentityErrors!);
                return View();
            }
            if(result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View();
            }
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(_mapper.Map<AppUser>(result.Data), true);
            TempData["StatusMessage"] = "Üye bilgileri başarılı bir şekilde güncellenmiştir.";
            return View(result.Data);
        }
        // Kullanıcı Ekleme, Güncelleme, Silme Action Methodları geliştirilecek,
        // Role Güncelleme, Silme action methodları RolesController'da geliştirilecek, 
        // İlgili kullanıcıya Role Atama action methodu  RolesController'da geliştirilecek !!!!!
    }
}
