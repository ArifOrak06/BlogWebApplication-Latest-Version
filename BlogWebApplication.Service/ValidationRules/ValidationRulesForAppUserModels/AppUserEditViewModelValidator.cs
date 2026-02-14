using BlogWebApplication.Core.Models.AppUserModels;
using FluentValidation;

namespace BlogWebApplication.Service.ValidationRules.ValidationRulesForAppUserModels
{
    public class AppUserEditViewModelValidator : AbstractValidator<AppUserEditViewModel>
    {
        public AppUserEditViewModelValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(3)
             .WithMessage("Minimum 3 karakterden oluşturulmalıdır.").WithName("Kullanıcı Adı");
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(10)
                .WithMessage("Minimum 10 karakterden oluşturulmalıdır.").WithName("E-Posta");
         
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("{PropertyName} alanı bilgi girilmesi zorunlu bir alandır.").MinimumLength(9)
                .WithMessage("Minimum 9 Karakterden oluşturulmalıdır.").WithName("GSM");

          
        }
    }
}
