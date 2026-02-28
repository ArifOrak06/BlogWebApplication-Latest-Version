using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace BlogWebApplication.WebUI.TagHelpers
{
    public class UserRoleNameTagHelper : TagHelper
    {
        public string UserId { get; set; } = null!;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;

        public UserRoleNameTagHelper(UserManager<AppUser> userManager, IAppUserService appUserService)
        {
            _userManager = userManager;
            _appUserService = appUserService;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            AppUser currentUser = (await _userManager.FindByIdAsync(UserId))!;

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var stringBuilder = new StringBuilder();

            userRoles.ToList().ForEach(role =>
            {
                stringBuilder.Append(@$"<span class='badge bg-warning ml-1'>{role.ToLower()}</span>");
            });

            output.Content.SetHtmlContent(stringBuilder.ToString());
         

            
        }
    }
}
