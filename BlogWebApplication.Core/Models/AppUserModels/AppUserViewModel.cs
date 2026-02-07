using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.ArticleModels;

namespace BlogWebApplication.Core.Models.AppUserModels
{
    public class AppUserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }= null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public ICollection<ArticleViewModel> Articles { get; set; }
        public Img Img { get; set; }
        public Guid ImgId { get; set; }
    }
}
