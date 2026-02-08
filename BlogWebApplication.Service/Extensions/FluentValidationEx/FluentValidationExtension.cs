using BlogWebApplication.SharedLibrary.RRP;
using FluentValidation.Results;

namespace BlogWebApplication.Service.Extensions.FluentValidationEx
{
    public static class FluentValidationExtension
    {
        public static List<CustomValidationError> ConvertToCustomValidationErrors(this ValidationResult result)
        {
            List<CustomValidationError> customValidationErrors = new();
            foreach (var validError in result.Errors)
            {
                customValidationErrors.Add(new CustomValidationError { PropertyName = validError.PropertyName, ErrorMessage = validError.ErrorMessage });
            }
            return customValidationErrors;
        }
    }
}
