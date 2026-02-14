using BlogWebApplication.Core.Models.AppUserModels;
using FluentValidation;

namespace BlogWebApplication.Service.ValidationRules.ValidationRulesForAppUserModels
{
    public class AppUserPasswordChangeViewModelValidator : AbstractValidator<AppUserPasswordChangeViewModel>
    {
        public AppUserPasswordChangeViewModelValidator()
        {
            RuleFor(x => x.CurrentPassword).NotNull().WithMessage("{PropertyName} alanı boş olamaz.");
            RuleFor(x => x.NewPassword).NotNull().WithMessage("{PropertyName} alanı boş olamaz.");
            RuleFor(x => x.NewPasswordConfirm).NotNull().WithMessage("{PropertyName} alanı boş olamaz.");
        }
    }
}
