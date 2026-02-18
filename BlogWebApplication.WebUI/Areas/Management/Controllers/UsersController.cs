using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
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

        public UsersController(IAppUserService appUserService, UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            CustomResponseModel<List<AppUserViewModel>> result = await _appUserService.GetAllAppUsersWithArticlesAndImgAsync();
            if (result.ResponseType == ResponseType.NotFound)
                return NotFound();

            return View(result.Data);
        }
    }
}
