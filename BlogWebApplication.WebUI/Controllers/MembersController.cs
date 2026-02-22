using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using BlogWebApplication.WebUI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.WebUI.Controllers
{
    public class MembersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
       
        public MembersController(SignInManager<AppUser> signInManager, IAppUserService appUserService, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _appUserService = appUserService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            CustomResponseModel<AppUserViewModel>? result = await _appUserService.GetAppUserWithArticlesAndImgByUserNameAsync(User.Identity!.Name!);
            if (result.ResponseType == ResponseType.NotFound)
                return NotFound();
            return View(result.Data);
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IActionResult> PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(AppUserPasswordChangeViewModel request)
        {
            CustomResponseModel<NoContentModel> result = await _appUserService.PasswordChangeToAppUserAsync(request);
            if(result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View();
            }
            if(result.ResponseType == ResponseType.IdentityError)
            {
                ModelState.AddModelStateIdentityErrorList(result.IdentityErrors!);
                return View();
            }
            if (result.ResponseType == ResponseType.Error)
            {
                TempData["StatusMessage"] = result.Errors!.First().ToString();
                return View();
            }
            TempData["StatusMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(PasswordChange));

        }

    }
}
