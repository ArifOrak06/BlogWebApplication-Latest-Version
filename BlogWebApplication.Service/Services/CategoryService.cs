using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.CategoryModels;
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
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<CategoryCreateViewModel> _categoryCreateValidator;
        private readonly IValidator<CategoryUpdateViewModel> _categoryUpdateValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper, IUow uow, IValidator<CategoryCreateViewModel> categoryCreateValidator, IValidator<CategoryUpdateViewModel> categoryUpdateValidator, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _uow = uow;
            _categoryCreateValidator = categoryCreateValidator;
            _categoryUpdateValidator = categoryUpdateValidator;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipal = _httpContextAccessor.HttpContext!.User;
        }

        public async Task<CustomResponseModel<CategoryCreateViewModel>> CreateOneCategoryAsync(CategoryCreateViewModel request)
        {
            var validationResult = await _categoryCreateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return CustomResponseModel<CategoryCreateViewModel>.ValidationFail(ResponseType.ValidationError, validationResult.ConvertToCustomValidationErrors());
            Category? newCategory = _mapper.Map<Category>(request);
            newCategory.IsActive = true;
            newCategory.IsDeleted = false;
            newCategory.CreatedDate = DateTime.Now;
            newCategory.ModifiedDate = DateTime.Now;
            newCategory.ModifiedBy = _claimsPrincipal.GetLoggedInUserEmail();
            newCategory.CreatedBy = _claimsPrincipal.GetLoggedInUserEmail();
            await _repositoryManager.CategoryRepository.CreateAsync(newCategory);
            await _uow.CommitAsync();
            return CustomResponseModel<CategoryCreateViewModel>.Success(ResponseType.Success, _mapper.Map<CategoryCreateViewModel>(newCategory), "Kategori başarıyla oluşturuldu.");
        }

        public async Task<CustomResponseModel<NoContentModel>> DeleteOneCategoryAsync(Guid categoryId)
        {
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(true, x => x.Id.Equals(categoryId)).SingleOrDefaultAsync();
            if (currentCategory is null)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.NotFound, "Kategori bulunamadı.");
            _repositoryManager.CategoryRepository.Delete(currentCategory);
            await _uow.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success, "Kategori kalıcı olarak başarıyla silindi.");
        }

        public async Task<CustomResponseModel<List<CategoryViewModel>>> GetAllActiveCategoriesWithArticlesAsync()
        {
            List<Category>? categories = await _repositoryManager.CategoryRepository.GetByFilter(false,x => x.IsActive && !x.IsDeleted, x => x.Articles).ToListAsync();
            if(categories is null || categories.Count == 0)
                return CustomResponseModel<List<CategoryViewModel>>.Fail(ResponseType.NotFound, "Aktif kategori bulunamadı.");
            return CustomResponseModel<List<CategoryViewModel>>.Success(ResponseType.Success, _mapper.Map<List<CategoryViewModel>>(categories), "Aktif kategoriler başarıyla getirildi.");
        }

        public async Task<CustomResponseModel<List<CategoryViewModel>>> GetAllSoftDeletedCategoriesWithArticlesAsync()
        {
            List<Category>? categories = await _repositoryManager.CategoryRepository.GetByFilter(false,x => x.IsDeleted &&  !x.IsActive,x => x.Articles).ToListAsync();
            if (categories is null || categories.Count == 0)
                return CustomResponseModel<List<CategoryViewModel>>.Fail(ResponseType.NotFound, "Silinmiş kategori bulunamadı.");
            return CustomResponseModel<List<CategoryViewModel>>.Success(ResponseType.Success, _mapper.Map<List<CategoryViewModel>>(categories), "Arayüzden silinmiş pasif kategoriler başarıyla getirildi.");
        }

  
        public async Task<CustomResponseModel<CategoryViewModel>> GetOneCategoryWithArticlesByCategoryIdAsync(Guid categoryId)
        {
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(false,x => x.Id.Equals(categoryId), x => x.Articles).SingleOrDefaultAsync();
            if (currentCategory is null)
                return CustomResponseModel<CategoryViewModel>.Fail(ResponseType.NotFound, "Kategori bulunamadı.");
            return CustomResponseModel<CategoryViewModel>.Success(ResponseType.Success, _mapper.Map<CategoryViewModel>(currentCategory), "Kategori başarıyla getirildi.");
        }

        public async Task<CustomResponseModel<NoContentModel>> RestoreOneCategoryAsync(Guid categoryId)
        {
            Category? softDeletedCategory = await _repositoryManager.CategoryRepository.GetByFilter(true, x => x.Id.Equals(categoryId) && x.IsDeleted).SingleOrDefaultAsync();
            if(softDeletedCategory is null)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.NotFound, "Silinmiş kategori bulunamadı.");
            softDeletedCategory.IsDeleted = false;
            softDeletedCategory.IsActive = true;
            softDeletedCategory.ModifiedDate = DateTime.Now;
            softDeletedCategory.ModifiedBy = _claimsPrincipal.GetLoggedInUserEmail();
            await _uow.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success, "Kategori başarıyla geri yüklendi.");
        }

        public async Task<CustomResponseModel<NoContentModel>> SoftDeleteOneCategoryAsync(Guid categoryId)
        {
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(true, x => x.Id.Equals(categoryId)).SingleOrDefaultAsync();
            if (currentCategory is null)
                return CustomResponseModel<NoContentModel>.Fail(ResponseType.NotFound, "Silinmiş kategori bulunamadı.");
            currentCategory.IsDeleted = true;
            currentCategory.IsActive = false;
            currentCategory.DeletedBy = _claimsPrincipal.GetLoggedInUserEmail();
            currentCategory.ModifiedDate = DateTime.Now;
            currentCategory.ModifiedBy = _claimsPrincipal.GetLoggedInUserEmail();
            await _uow.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(ResponseType.Success, "Kategori başarıyla arayüzden silindi.");
        }

        public async Task<CustomResponseModel<CategoryUpdateViewModel>> UpdateOneCategoryAsync(CategoryUpdateViewModel request)
        {
            var validationResult = await _categoryUpdateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                CategoryUpdateViewModel? updateModel = _mapper.Map<CategoryUpdateViewModel>(await _repositoryManager.CategoryRepository.GetByFilter(false, x => x.Id.Equals(request.Id)).SingleOrDefaultAsync());
                return CustomResponseModel<CategoryUpdateViewModel>.ValidationUpdateFail(ResponseType.ValidationError, updateModel!, validationResult.ConvertToCustomValidationErrors());
            }
            Category? currentCategory = await _repositoryManager.CategoryRepository.GetByFilter(true,x => x.Id.Equals(request.Id)).SingleOrDefaultAsync();
            if (currentCategory is null)
                return CustomResponseModel<CategoryUpdateViewModel>.Fail(ResponseType.NotFound, "Kategori bulunamadı.");
            currentCategory.Name = request.Name;
            currentCategory.IsActive = request.IsActive;
            currentCategory.IsDeleted  = request.IsActive ? false : true;
            currentCategory.ModifiedDate = DateTime.Now;
            currentCategory.ModifiedBy = _claimsPrincipal.GetLoggedInUserEmail();
            await _uow.CommitAsync();
            return CustomResponseModel<CategoryUpdateViewModel>.Success(ResponseType.Success, _mapper.Map<CategoryUpdateViewModel>(currentCategory), "Kategori başarıyla güncellendi.");

        }
    }
}
