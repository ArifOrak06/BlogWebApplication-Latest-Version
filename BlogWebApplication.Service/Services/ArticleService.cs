using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Helpers;
using BlogWebApplication.Core.Models.ArticleModels;
using BlogWebApplication.Core.Models.ImgModels;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Core.Services;
using BlogWebApplication.Core.Utilities.Uow;
using BlogWebApplication.Service.Extensions.FluentValidationEx;
using BlogWebApplication.Service.Extensions.IdentityEx;
using BlogWebApplication.SharedLibrary.Enums;
using BlogWebApplication.SharedLibrary.RRP;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogWebApplication.Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IImgHelper _imgHelper;
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<ArticleCreateViewModel> _createViewModelValidator;
        private readonly IValidator<ArticleUpdateViewModel> _updateViewModelValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public ArticleService(IRepositoryManager repositoryManager, IImgHelper imgHelper, IMapper mapper, IUow uow, IValidator<ArticleCreateViewModel> createViewModelValidator, IValidator<ArticleUpdateViewModel> updateViewModelValidator, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _imgHelper = imgHelper;
            _mapper = mapper;
            _uow = uow;
            _createViewModelValidator = createViewModelValidator;
            _updateViewModelValidator = updateViewModelValidator;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipal = _httpContextAccessor.HttpContext.User;
        }

        public async Task<CustomResponseModel<ArticleCreateViewModel>> CreateOneArticleAsync(ArticleCreateViewModel articleCreateViewModel)
        {
            var result = await _createViewModelValidator.ValidateAsync(articleCreateViewModel);
            if (!result.IsValid)
                return CustomResponseModel<ArticleCreateViewModel>.ValidationFail(ResponseType.ValidationError, result.ConvertToCustomValidationErrors());
            Article? newArticle = _mapper.Map<Article>(articleCreateViewModel);
            newArticle.AppUserId = _claimsPrincipal.GetLoggedInUserId();
            newArticle.CreatedBy = _claimsPrincipal.GetLoggedInUserName();
            newArticle.CategoryId = articleCreateViewModel.CategoryId;
            newArticle.IsActive = true;
            newArticle.IsDeleted = false;
            newArticle.CreatedDate = DateTime.Now;
            newArticle.ModifiedDate = DateTime.Now;
            if (articleCreateViewModel.Photo is not null)
            {
                CustomResponseModel<ImgUploadViewModel> imgUploadResult = await _imgHelper.UploadOneImageAsync(articleCreateViewModel.Photo)!;
                if (imgUploadResult.ResponseType == ResponseType.Success)
                {
                    Img? newImg = new Img()
                    {

                        CreatedDate = DateTime.Now,
                        FileName = imgUploadResult.Data!.FullName!,
                        FileType = imgUploadResult.Data.FileType!,
                        IsActive = true,
                        IsDeleted = false,
                        ModifiedDate = DateTime.Now,
                        CreatedBy = _claimsPrincipal.GetLoggedInUserName(),

                    };
                    await _repositoryManager.ImgRepository.CreateAsync(newImg);
                    await _uow.CommitAsync();
                    newArticle.ImgId = newImg.Id;
                }

            }
            await _repositoryManager.ArticleRepository.CreateAsync(newArticle);
            await _uow.CommitAsync();
            return CustomResponseModel<ArticleCreateViewModel>.Success(ResponseType.Success, _mapper.Map<ArticleCreateViewModel>(newArticle), $"{articleCreateViewModel.Title} başlıklı makale başarılı bir şekilde eklenmiştir.");

        }

        public async Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesAndNonDeletedArticlesWithCategoryAndAppUserAsync()
        {
            List<Article>? articles = await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.IsActive && !x.IsDeleted, x => x.AppUser, x => x.Category, x => x.Img).ToListAsync();
            if (articles is null)
                return CustomResponseModel<List<ArticleViewModel>>.Fail(ResponseType.NotFound, "Sistemde kayıtlı aktif makale bulunmamaktadır.");
            return CustomResponseModel<List<ArticleViewModel>>.Success(ResponseType.Success, _mapper.Map<List<ArticleViewModel>>(articles), "Sistemde kayıtlı aktif makaleler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithAppUserByCategoryIdAsync(Guid categoryId)
        {
            List<Article>? articles = await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.CategoryId == categoryId&&x.IsActive && !x.IsDeleted, x => x.AppUser, x => x.Category, x => x.Img).ToListAsync();
            if (articles is null)
                return CustomResponseModel<List<ArticleViewModel>>.Fail(ResponseType.NotFound, $"Kategori Id : {categoryId} olan kategoriye ait Sistemde kayıtlı aktif makale bulunmamaktadır.");
            return CustomResponseModel<List<ArticleViewModel>>.Success(ResponseType.Success, _mapper.Map<List<ArticleViewModel>>(articles), $"Kategori Id : {categoryId} olan kategoriye ait makaleler başarıyla listelenmiştir.");
        }

        public async Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithCategoryByAppUserIdAsync(Guid appUserId)
        {
            List<Article>? articles = await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.AppUserId == appUserId && x.IsActive && !x.IsDeleted, x => x.AppUser, x => x.Category, x => x.Img).ToListAsync();
            if (articles is null)
                return CustomResponseModel<List<ArticleViewModel>>.Fail(ResponseType.NotFound, $"Kullanıcı Id : {appUserId} olan kullanıcıya ait Sistemde kayıtlı aktif makale bulunmamaktadır.");
            return CustomResponseModel<List<ArticleViewModel>>.Success(ResponseType.Success, _mapper.Map<List<ArticleViewModel>>(articles), $"AppUser Id : {appUserId} olan kullanıcıya ait makaleler başarıyla listelenmiştir.");
        }

        public async Task<CustomResponseModel<List<ArticleViewModel>>> GetAllDeletedArticlesWithCategoryAndAppUserAsync()
        {
            List<Article>? articles = await _repositoryManager.ArticleRepository.GetByFilter(false, x => !x.IsActive && x.IsDeleted, x => x.AppUser, x => x.Category, x => x.Img).ToListAsync();
            if (articles is null)
                return CustomResponseModel<List<ArticleViewModel>>.Fail(ResponseType.NotFound, "Sistemde kayıtlı pasif makale bulunmamaktadır.");
            return CustomResponseModel<List<ArticleViewModel>>.Success(ResponseType.Success, _mapper.Map<List<ArticleViewModel>>(articles), "Sistemde kayıtlı pasif makaleler başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseModel<ArticleViewModel>> GetOneActiveArticleWithCategoryAndAppUserByArticleIdAsync(Guid articleId)
        {
            Article? article = await _repositoryManager.ArticleRepository.GetByFilter(false, x => x.Id  == articleId && x.IsActive && !x.IsDeleted, x => x.AppUser, x => x.Category, x => x.Img).SingleOrDefaultAsync();
            if (article is null)
                return CustomResponseModel<ArticleViewModel>.Fail(ResponseType.NotFound, "Sistemde kayıtlı pasif makale bulunmamaktadır.");
            return CustomResponseModel<ArticleViewModel>.Success(ResponseType.Success,_mapper.Map<ArticleViewModel>(article),$"Makale ID : {articleId} olan makale başarılı bir şekilde listelenmiştir.");
        }

        public async Task<CustomResponseModel<NoContentModel>> HardDeleteOneArticleAsync(Guid articleId)
        {
            Article? article = await _repositoryManager.ArticleRepository.GetByFilter(true,x => x.Id.Equals(articleId)).SingleOrDefaultAsync();
            if (article is null)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.NotFound, $"Makale Id : {articleId} olan makale bulunamamıştır.");
            _repositoryManager.ArticleRepository.Delete(article);
            await _uow.CommitAsync();   
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success,$"Makale Id : {articleId} olan makale başarılı bir şekilde kalıcı olarak silinmiştir.");
        }

        public async Task<CustomResponseModel<NoContentModel>> RestoreOneArticleAsync(Guid articleId)
        {
            Article? article = await _repositoryManager.ArticleRepository.GetByFilter(true, x => x.Id.Equals(articleId)).SingleOrDefaultAsync();
            if (article is null)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.NotFound, $"Makale Id : {articleId} olan makale bulunamamıştır.");
            article.IsDeleted = false;
            article.IsActive = true;
            article.ModifiedBy = _claimsPrincipal.GetLoggedInUserEmail();
            article.ModifiedDate = DateTime.Now;
            await _uow.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success, $"Makale Id : {articleId} olan makale başarılı bir şekilde aktif hale  getirilmiştir.");
        }

        public async  Task<CustomResponseModel<NoContentModel>> SoftDeleteOneArticleAsync(Guid articleId)
        {
            Article? article = await _repositoryManager.ArticleRepository.GetByFilter(true, x => x.Id.Equals(articleId)).SingleOrDefaultAsync();
            if (article is null)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.NotFound, $"Makale Id : {articleId} olan makale bulunamamıştır.");
            article.IsActive = false;
            article.IsDeleted = true;
            article.ModifiedDate = DateTime.Now;
            article.ModifiedBy = _claimsPrincipal.GetLoggedInUserEmail();
            await _uow.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success, $"Makale Id : {articleId} olan makale başarılı bir şekilde pasif hale getirilmiştir.");
        }

        public async Task<CustomResponseModel<ArticleUpdateViewModel>> UpdateOneArticleAsync(ArticleUpdateViewModel articleUpdateViewModel)
        {
            var result = await _updateViewModelValidator.ValidateAsync(articleUpdateViewModel);
            if (!result.IsValid)
            {
                ArticleUpdateViewModel newModel = _mapper.Map<ArticleUpdateViewModel>(await _repositoryManager.ArticleRepository
                    .GetByFilter(false, x => x.Id.Equals(articleUpdateViewModel.Id), x => x.Category, x => x.AppUser, x => x.Img).SingleOrDefaultAsync());
                return CustomResponseModel<ArticleUpdateViewModel>.ValidationUpdateFail(ResponseType.ValidationError, newModel, result.ConvertToCustomValidationErrors());
            }
            Article currentArticle = await _repositoryManager.ArticleRepository.GetByFilter(true, x => x.Id.Equals(articleUpdateViewModel.Id), x => x.Img, x => x.AppUser, x => x.Category).SingleOrDefaultAsync();
            if (currentArticle is null)
                return CustomResponseModel<ArticleUpdateViewModel>.Fail(ResponseType.NotFound, $"Makale Id : {articleUpdateViewModel.Id} olan makale sistemde kayıtlı değildir.");
            currentArticle.Title = articleUpdateViewModel.Title;
            currentArticle.Content = articleUpdateViewModel.Content;
            currentArticle.CategoryId = articleUpdateViewModel.CategoryId;
            currentArticle.IsActive = articleUpdateViewModel.IsActive;
            currentArticle.IsDeleted = articleUpdateViewModel.IsActive ? false : true;
            currentArticle.ModifiedDate = DateTime.Now;
            currentArticle.ModifiedBy = _claimsPrincipal.GetLoggedInUserEmail();
            if(articleUpdateViewModel.Photo is not null)
            {
                CustomResponseModel<ImgUploadViewModel> imgUploadResult = await _imgHelper.UploadOneImageAsync(articleUpdateViewModel.Photo);
                if (imgUploadResult.ResponseType == ResponseType.Success)
                {
                    Img? newImg = new Img()
                    {

                        CreatedDate = DateTime.Now,
                        FileName = imgUploadResult.Data.FullName,
                        FileType = articleUpdateViewModel.Photo.ContentType,
                        IsActive = true,
                        IsDeleted = false,
                        ModifiedDate = DateTime.Now,
                        CreatedBy = _claimsPrincipal.GetLoggedInUserName(),

                    };
                    await _repositoryManager.ImgRepository.CreateAsync(newImg);
                    await _uow.CommitAsync();
                    currentArticle.ImgId = newImg.Id;
                }
            }
            await _uow.CommitAsync();
            return CustomResponseModel<ArticleUpdateViewModel>.Success(ResponseType.Success, _mapper.Map<ArticleUpdateViewModel>(currentArticle), $"Makale Id : {articleUpdateViewModel.Id} olan makale başarıyla güncellendi.");
        }
    }
}
