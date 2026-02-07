using BlogWebApplication.Core.Models.CategoryModels;
using FluentValidation;

namespace BlogWebApplication.Service.ValidationRules.ValidationRulesForCategoryModels
{
    public class CategoryUpdateViewModelValidator : AbstractValidator<CategoryUpdateViewModel>
    {
        public CategoryUpdateViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.")
         .MinimumLength(10).WithMessage("{PropertyName} alanı minimum 10 karakterden oluşmalıdır.")
         .MaximumLength(200).WithMessage("{PropertyName} alanı maksimum 200 karakterden oluşmalıdır.").WithName("Kategori Adı");
           RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.");
        }
    }
}
