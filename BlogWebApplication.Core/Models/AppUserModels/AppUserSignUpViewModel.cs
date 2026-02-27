using Microsoft.AspNetCore.Http;

namespace BlogWebApplication.Core.Models.AppUserModels
{
    public class AppUserSignUpViewModel
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public IFormFile Photo { get; set; }
    }
}
