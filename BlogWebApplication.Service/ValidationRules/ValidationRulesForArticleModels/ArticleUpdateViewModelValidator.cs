using BlogWebApplication.Core.Models.ArticleModels;
using FluentValidation;

namespace BlogWebApplication.Service.ValidationRules.ValidationRulesForArticleModels
{
    public class ArticleUpdateViewModelValidator : AbstractValidator<ArticleUpdateViewModel>
    {
        public ArticleUpdateViewModelValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.")
            .MinimumLength(10).WithMessage("{PropertyName} alanı minimum 10 karakterden oluşmalıdır.")
            .MaximumLength(200).WithMessage("{PropertyName} alanı maksimum 200 karakterden oluşmalıdır.").WithName("Baslık");
            RuleFor(x => x.Content).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.")
               .MinimumLength(10).WithMessage("{PropertyName} alanı minimum 10 karakterden oluşmalıdır.").WithName("İçerik");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("{PropertyName} alanı seçilmesi zorunlu bir alandır.");
            RuleFor(x => x.Photo).NotNull().WithMessage("{PropertyName} alanına bir fotoğraf yüklenmesi zorunludur.");
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.");
        }
    }
}
