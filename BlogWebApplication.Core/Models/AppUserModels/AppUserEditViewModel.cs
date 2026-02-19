using BlogWebApplication.Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace BlogWebApplication.Core.Models.AppUserModels
{
    public class AppUserEditViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public Img Img { get; set; }
        public Guid? ImgId { get; set; }
        public IFormFile Photo { get; set; }

    }
}
