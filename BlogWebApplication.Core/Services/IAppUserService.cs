using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.SharedLibrary.RRP;

namespace BlogWebApplication.Core.Services
{
    public interface IAppUserService
    {
        Task<CustomResponseModel<List<AppUserViewModel>>> GetAllAppUsersWithArticlesAndImgAsync();
        Task<CustomResponseModel<AppUserViewModel>> GetAppUserWithArticlesAndImgByUserIdAsync(Guid appUserId);
        Task<CustomResponseModel<AppUserSignUpViewModel>> CreateOneAppUserAsync(AppUserSignUpViewModel appUserSignUpViewModel);
        Task<CustomResponseModel<AppUserEditViewModel>> UpdateOneAppUserAsync(AppUserEditViewModel appUserEditViewModel);
        Task<CustomResponseModel<NoContentModel>> DeleteOneAppUserAsync(Guid appUserId);
        Task<CustomResponseModel<NoContentModel>> PasswordChangeToAppUserAsync(AppUserPasswordChangeViewModel appUserPasswordChangeViwModel);
    }
}
