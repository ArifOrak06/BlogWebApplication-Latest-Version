using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using BlogWebApplication.WebUI.Extensions;
using BlogWebApplication.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogWebApplication.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, IAppUserService appUserService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _appUserService = appUserService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserSignInViewModel request, string? returnUrl)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                ModelState.AddModelError(string.Empty, "Email veya şifre hatalı");
            var singInResult = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (singInResult.Succeeded)
                return Redirect(returnUrl);

            

            // Kullanıcının Hesabı kilitlenmişse;

            if (singInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Hesabınız bir süreliğine kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");
                return View();
            }

            ModelState.AddModelError(string.Empty, $"Email veya şifre hatalı, başarısız giriş denemesi : {await _userManager.GetAccessFailedCountAsync(user)}");

            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserSignUpViewModel request)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _appUserService.CreateOneAppUserAsync(request);
            if (result.ResponseType == ResponseType.IdentityError)
            {
                ModelState.AddModelStateIdentityErrorList(result.IdentityErrors!);
                return View();
            }
            if (result.ResponseType == ResponseType.ValidationError)
            {
                ModelState.AddModelStateValidationErrorList(result.ValidationErrors!);
                return View();
            }
            TempData["SuccessMessage"] = result.isSuccessMessage;
            return RedirectToAction(nameof(HomeController.SignUp));

        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
