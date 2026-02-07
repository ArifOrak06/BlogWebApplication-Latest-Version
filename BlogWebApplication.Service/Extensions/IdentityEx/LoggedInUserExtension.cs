using System.Security.Claims;

namespace BlogWebApplication.Service.Extensions.IdentityEx
{
    public static class LoggedInUserExtension
    {
        public static Guid GetLoggedInUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public static string GetLoggedInUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Email)!;
        }
         public static string GetLoggedInUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Name)!;
        }
    }
}
