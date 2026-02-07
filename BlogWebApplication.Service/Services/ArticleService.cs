using AutoMapper;
using BlogWebApplication.Core.Helpers;
using BlogWebApplication.Core.Models.ArticleModels;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Core.Services;
using BlogWebApplication.Core.Utilities.Uow;
using BlogWebApplication.SharedLibrary.RRP;
using FluentValidation;
using Microsoft.AspNetCore.Http;
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

        public Task<CustomResponseModel<ArticleCreateViewModel>> CreateOneArticleAsync(ArticleCreateViewModel articleCreateViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesAndNonDeletedArticlesWithCategoryAndAppUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithAppUserByCategoryIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllActivesArticlesWithCategoryByAppUserIdAsync(Guid appUserId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<List<ArticleViewModel>>> GetAllDeletedArticlesWithCategoryAndAppUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<ArticleViewModel>> GetOneActiveArticleWithCategoryAndAppUserByArticleIdAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<NoContentModel>> HardDeleteOneArticleAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<NoContentModel>> RestoreOneArticleAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<NoContentModel>> SoftDeleteOneArticleAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseModel<ArticleUpdateViewModel>> UpdateOneArticleAsync(ArticleUpdateViewModel articleCreateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
