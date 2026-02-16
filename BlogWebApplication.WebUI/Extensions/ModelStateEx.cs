using BlogWebApplication.SharedLibrary.RRP;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogWebApplication.WebUI.Extensions
{
    public static class ModelStateEx
    {
        public static void AddModelStateIdentityErrorList(this ModelStateDictionary modelState, List<CustomIdentityError> identityErrors)
        {

            foreach (var error in identityErrors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }



        }
        public static void AddModelStateValidationErrorList(this ModelStateDictionary modelState, List<CustomValidationError> validationErrors = null)
        {

            foreach (var error in validationErrors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

        }
    }
}
