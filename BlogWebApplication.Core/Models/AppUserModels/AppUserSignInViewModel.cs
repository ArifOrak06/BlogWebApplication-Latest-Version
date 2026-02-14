namespace BlogWebApplication.Core.Models.AppUserModels
{
    public class AppUserSignInViewModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
