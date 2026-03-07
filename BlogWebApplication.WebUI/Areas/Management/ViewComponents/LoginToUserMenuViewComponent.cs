using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.Core.Services;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using BlogWebApplication.WebUI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApplication.WebUI.Areas.Management.ViewComponents
{
    public class LoginToUserMenuViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public LoginToUserMenuViewComponent(IAppUserService appUserService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CustomResponseModel<AppUserViewModel> result = await _appUserService.GetAppUserWithArticlesAndImgByUserNameAsync(User.Identity!.Name!);
            if (result.ResponseType == ResponseType.IdentityError)
            {
                ModelState.AddModelStateIdentityErrorList(result.IdentityErrors!);
                return View();
            }
            var userRoles = await _userManager.GetRolesAsync(_mapper.Map<AppUser>(result.Data));
            if (userRoles.Any())
            {
                result.Data!.Roles = userRoles;
                return View(result.Data);
            }
            result.Data!.Roles = new List<String>() { "Atanmadı" };
            return View(result.Data);
        }
    }
}
