using BlogWebApplication.Core.Models.AppUserModels;
using FluentValidation;

namespace BlogWebApplication.Service.ValidationRules.ValidationRulesForAppUserModels
{
    public class AppUserSignUpViewModelValidator : AbstractValidator<AppUserSignUpViewModel>
    {
        public AppUserSignUpViewModelValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(3)
                .WithMessage("Minimum 3 karakterden oluşturulmalıdır.").WithName("Kullanıcı Adı");
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(10)
                .WithMessage("Minimum 10 karakterden oluşturulmalıdır.").WithName("E-Posta");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} alanı belirlenmesi gereken zorunlu bir alandır.").MinimumLength(5)
            .WithMessage("Minimum 5 karakterden oluşturulmalıdır.").WithName("Parola");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("{PropertyName} alanı belirlenmesi gereken zorunlu bir alandır.").MinimumLength(5)
          .WithMessage("Minimum 5 karakterden oluşturulmalıdır.").WithName("Parola Tekrar");
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(9)
                .WithMessage("Minimum 9 Karakterden oluşturulmalıdır.");


        }
    }
}
