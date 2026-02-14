using BlogWebApplication.Core.Models.AppUserModels;
using FluentValidation;

namespace BlogWebApplication.Service.ValidationRules.ValidationRulesForAppUserModels
{
    public class AppUserSignInViewModelValidator : AbstractValidator<AppUserSignInViewModel>
    {
        public AppUserSignInViewModelValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(10)
                .WithMessage("Minimum 10 karakterden oluşturulmalıdır.").WithName("E-Posta");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").WithName("Parola");
        }
    }
}
