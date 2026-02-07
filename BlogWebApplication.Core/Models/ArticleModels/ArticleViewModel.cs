using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.AppUserModels;
using BlogWebApplication.Core.Models.CategoryModels;

namespace BlogWebApplication.Core.Models.ArticleModels
{
    public class ArticleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }
        public AppUserViewModel AppUser { get; set; }
        public Guid AppUserId { get; set; }
        public string? PictureUrl { get; set; }
        public Img  Img { get; set; }
        public Guid ImgId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
