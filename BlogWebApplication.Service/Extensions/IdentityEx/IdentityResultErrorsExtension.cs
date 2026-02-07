using BlogWebApplication.SharedLibrary.RRP;
using Microsoft.AspNetCore.Identity;

namespace BlogWebApplication.Service.Extensions.IdentityEx
{
    public static class IdentityResultErrorsExtension
    {
        public static List<CustomIdentityError> ConvertToCustomIdentityError(this IdentityResult result)
        {
            List<CustomIdentityError>? customIdentityErrors = new();
            foreach (var identityError in result.Errors)
            {
                customIdentityErrors.Add(new CustomIdentityError
                {
                    Description = identityError.Description,
                });
            }
            return customIdentityErrors;
        }
    }
}
