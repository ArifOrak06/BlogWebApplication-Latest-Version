using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Helpers;
using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Core.Services;
using BlogWebApplication.Core.Utilities.Uow;
using BlogWebApplication.Service.Extensions.FluentValidationEx;
using BlogWebApplication.Service.Extensions.IdentityEx;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogWebApplication.Service.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IImgHelper _imgHelper;
        private readonly IValidator<AppUserSignUpViewModel> _appUserSignUpValidator;
        private readonly IValidator<AppUserEditViewModel> _appUserEditValidator;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimPrincipal;
        private readonly IValidator<AppUserSignInViewModel> _appUserSignInValidator;
        private readonly IValidator<AppUserPasswordChangeViewModel> _appUserPasswordChangeValidator;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IRepositoryManager repositoryManager, IMapper mapper, IUow uow, IImgHelper imgHelper, IValidator<AppUserSignUpViewModel> appUserSignUpValidator, IValidator<AppUserEditViewModel> appUserEditValidator, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IValidator<AppUserSignInViewModel> appUserSignInValidator, IValidator<AppUserPasswordChangeViewModel> appUserPasswordChangeValidator, SignInManager<AppUser> signInManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _uow = uow;
            _imgHelper = imgHelper;
            _appUserSignUpValidator = appUserSignUpValidator;
            _appUserEditValidator = appUserEditValidator;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _claimPrincipal = _httpContextAccessor.HttpContext!.User;
            _appUserSignInValidator = appUserSignInValidator;
            _appUserPasswordChangeValidator = appUserPasswordChangeValidator;
            _signInManager = signInManager;
        }

        public async Task<CustomResponseModel<AppUserSignUpViewModel>> CreateOneAppUserAsync(AppUserSignUpViewModel appUserSignUpViewModel)
        {
            var validationResult = await _appUserSignUpValidator.ValidateAsync(appUserSignUpViewModel);
            if (!validationResult.IsValid)
                return CustomResponseModel<AppUserSignUpViewModel>.ValidationFail(ResponseType.ValidationError, validationResult.ConvertToCustomValidationErrors());
            AppUser? newUser = _mapper.Map<AppUser>(appUserSignUpViewModel);
            newUser.Email = appUserSignUpViewModel.Email;
            if(appUserSignUpViewModel.Photo is not null)
            {
                var imgResult = await _imgHelper.UploadOneImageAsync(appUserSignUpViewModel.Photo, appUserSignUpViewModel.Email);
                Img newImg = new()
                {
                    FileName = imgResult.Data!.FullName!,
                    FileType = imgResult.Data.FileType!,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = _claimPrincipal.GetLoggedInUserEmail(),
                    ModifiedBy = _claimPrincipal.GetLoggedInUserEmail(),
                    IsActive = true,
                    IsDeleted = false,

                };
                await _repositoryManager.ImgRepository.CreateAsync(newImg);
                await _uow.CommitAsync();
                if (imgResult.ResponseType == ResponseType.Success)
                    newUser.ImgId = newImg.Id;
            }

            IdentityResult? identityResult = await _userManager.CreateAsync(newUser, appUserSignUpViewModel.Password);
            if (!identityResult.Succeeded)
                return CustomResponseModel<AppUserSignUpViewModel>.IdentityFail(ResponseType.IdentityError, identityResult.ConvertToCustomIdentityError());
            return CustomResponseModel<AppUserSignUpViewModel>.Success(ResponseType.Success, _mapper.Map<AppUserSignUpViewModel>(newUser), "Kullanıcı başarılı bir şekilde oluşturuldu.");
        }

        public async Task<CustomResponseModel<NoContentModel>> DeleteOneAppUserAsync(Guid appUserId)
        {
            AppUser currentUser = await _repositoryManager.AppUserRepository.GetAppUserWithArticlesAndImgAsync(true, x => x.Id.Equals(appUserId), x => x.Articles, x => x.Img);
            if (currentUser is null)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.NotFound, $"Kullanıcı id : {appUserId}'ye sahip üye sistemde bulunamamıştır.");
            var identityResult = await _userManager.DeleteAsync(currentUser);
            if (!identityResult.Succeeded)
                return CustomResponseModel<NoContentModel>.IdentityFail(ResponseType.IdentityError, identityResult.ConvertToCustomIdentityError());
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success, $"Kullanıcı id :{appUserId} olan üye silme işlemi başarılı olarak gerçekleştirilmiştir.");


        }

        public async Task<CustomResponseModel<List<AppUserViewModel>>> GetAllAppUsersWithArticlesAndImgAsync()
        {
            List<AppUser>? appUsers = await _repositoryManager.AppUserRepository.GetAllAppUsersWithArticlesAndImgAsync(false, null, x => x.Articles, x => x.Img);
            if (appUsers == null)
                return CustomResponseModel<List<AppUserViewModel>>.Fail(ResponseType.NotFound, "Sistemde listelenecek kullanıcı bulunmamaktadır.");
            return CustomResponseModel<List<AppUserViewModel>>.Success(ResponseType.Success, _mapper.Map<List<AppUserViewModel>>(appUsers), "Kullanıcılar başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseModel<AppUserViewModel>> GetAppUserWithArticlesAndImgByUserIdAsync(Guid appUserId)
        {
            AppUser? currentUser = await _repositoryManager.AppUserRepository.GetAppUserWithArticlesAndImgAsync(false, x => x.Id.Equals(appUserId),x => x.Articles,x => x.Img);
            if (currentUser is null)
                return CustomResponseModel<AppUserViewModel>.Fail(ResponseType.NotFound, $"Kullanıcı Id : {appUserId} olan üye sistemde bulunamamıştır.");
            return CustomResponseModel<AppUserViewModel>.Success(ResponseType.Success, _mapper.Map<AppUserViewModel>(currentUser), $"Kullanıcı ID : {appUserId} olan kullanıcı başarılı bir şekilde listelenmiştir.");


        }

        public async Task<CustomResponseModel<AppUserViewModel>> GetAppUserWithArticlesAndImgByUserNameAsync(string userName)
        {
            //AppUser? currentUser = await _userManager.FindByNameAsync(userName);
            AppUser? currentUser = await _repositoryManager.AppUserRepository.GetAppUserWithArticlesAndImgAsync(false, x => x.UserName.Equals(userName), x => x.Articles, x => x.Img);
            if (currentUser is null)
                return CustomResponseModel<AppUserViewModel>.Fail(ResponseType.NotFound, $"Kullanıcı adı : {userName} olan üye sistemde bulunamamıştır.");
            return CustomResponseModel<AppUserViewModel>.Success(ResponseType.Success, _mapper.Map<AppUserViewModel>(currentUser), $"Kullanıcı adı :  {userName} olan kullanıcı başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseModel<NoContentModel>> PasswordChangeToAppUserAsync(AppUserPasswordChangeViewModel appUserPasswordChangeViwModel)
        {
            var validationResult = await _appUserPasswordChangeValidator.ValidateAsync(appUserPasswordChangeViwModel);
            if (!validationResult.IsValid)
                return CustomResponseModel<NoContentModel>.ValidationFail(ResponseType.ValidationError, validationResult.ConvertToCustomValidationErrors());
            var loginUserId = _claimPrincipal.GetLoggedInUserId();
            var currentAppUser = await _repositoryManager.AppUserRepository.GetAppUserWithArticlesAndImgAsync(true, x => x.Id.Equals(loginUserId), x => x.Articles, x => x.Img);
            //Old Password Check
            var checkPassword = await _userManager.CheckPasswordAsync(currentAppUser, appUserPasswordChangeViwModel.CurrentPassword);
            if (!checkPassword)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.Error, $"Eski şifre olarak girilen şifre ile sistemde kayıtlı şifreler uyuşmamaktadır.");
            var identityResult = await _userManager.ChangePasswordAsync(currentAppUser,appUserPasswordChangeViwModel.CurrentPassword,appUserPasswordChangeViwModel.NewPassword);
            if (!identityResult.Succeeded)
                return CustomResponseModel<NoContentModel>.IdentityFail(ResponseType.IdentityError, identityResult.ConvertToCustomIdentityError());
            // Parola hassas bir bilgi olduğu için SecurityStamp değeri yenilenmeli, kullanıcıya hissettirilmeden  logout ve tekrar login işlemleri yaptırılmalıdır.
            await _userManager.UpdateSecurityStampAsync(currentAppUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentAppUser,appUserPasswordChangeViwModel.NewPassword,true,false);
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success, "Şifre değişikliği başarılı bir şekilde gerçekleştirilmiştir.");



        }

        public async Task<CustomResponseModel<AppUserEditViewModel>> UpdateOneAppUserAsync(AppUserEditViewModel appUserEditViewModel)
        {
            var validationResult = await _appUserEditValidator.ValidateAsync(appUserEditViewModel);
            if (!validationResult.IsValid)
            {
                AppUserEditViewModel newUserEditModel = _mapper.Map<AppUserEditViewModel>(await _repositoryManager.AppUserRepository.GetAppUserWithArticlesAndImgAsync(false, x => x.Id.Equals(appUserEditViewModel.Id), x => x.Articles, x => x.Img));
                return CustomResponseModel<AppUserEditViewModel>.ValidationUpdateFail(ResponseType.ValidationError, newUserEditModel, validationResult.ConvertToCustomValidationErrors());
            }

            AppUser? currentUser = await _repositoryManager.AppUserRepository.GetAppUserWithArticlesAndImgAsync(true, x => x.Id.Equals(appUserEditViewModel.Id), x => x.Articles, x => x.Img);
            if (currentUser is null)
                return CustomResponseModel<AppUserEditViewModel>.Fail(ResponseType.NotFound, $"Kullanıcı Id : {appUserEditViewModel.Id} olan kullanıcı sistemde bulunamamıştır.");
            currentUser.Email = appUserEditViewModel.Email;
            currentUser.PhoneNumber = appUserEditViewModel.PhoneNumber;
            currentUser.UserName = appUserEditViewModel.UserName;
            if(appUserEditViewModel.Photo is not null)
            {
                var imgResult = await _imgHelper.UploadOneImageAsync(appUserEditViewModel.Photo);
                var newImg = new Img
                {
                    FileName = imgResult.Data!.FullName!,
                    FileType = imgResult.Data!.FileType!,
                    CreatedBy = _claimPrincipal.GetLoggedInUserEmail(),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = _claimPrincipal.GetLoggedInUserEmail()

                };
                await _repositoryManager.ImgRepository.CreateAsync(newImg);
                await _uow.CommitAsync();

                if (imgResult.ResponseType == ResponseType.Success)
                    currentUser.ImgId = newImg.Id;

            }
            // Resim güncelleme işleminden sonra kullanıcı güncellemeyi sonlandıralım
            var identityResult = await _userManager.UpdateAsync(currentUser);
            if (!identityResult.Succeeded)
                return CustomResponseModel<AppUserEditViewModel>.IdentityFail(ResponseType.IdentityError, identityResult.ConvertToCustomIdentityError());
            await _userManager.UpdateSecurityStampAsync(currentUser);

            return CustomResponseModel<AppUserEditViewModel>.Success(ResponseType.Success, _mapper.Map<AppUserEditViewModel>(currentUser), $"Kullanıcı Id : {appUserEditViewModel.Id} olan kullanıcı bilgileri başarılı bir şekilde güncellenmiştir.");
                
        }
    }
}
