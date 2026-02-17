using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.CategoryModels;
using Microsoft.AspNetCore.Http;

namespace BlogWebApplication.Core.Models.ArticleModels
{
    public class ArticleUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public ICollection<CategoryViewModel>? Categories { get; set; }
        public Guid? CategoryId { get; set; }
        public CategoryViewModel? Category { get; set; }
        public Guid? AppUserId { get; set; }
        public Guid? ImgId { get; set; }
        public Img?  Img { get; set; }
        public IFormFile? Photo { get; set; }
        public bool IsActive { get; set; }

    }
}
