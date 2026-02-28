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
            result.Data.Roles = await _userManager.GetRolesAsync(_mapper.Map<AppUser>(result.Data));
            return View(result.Data);
        }
    }
}
