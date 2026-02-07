using BlogWebApplication.Core.Models.CategoryModels;
using Microsoft.AspNetCore.Http;

namespace BlogWebApplication.Core.Models.ArticleModels
{
    public class ArticleCreateViewModel
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public ICollection<CategoryViewModel>? Categories { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AppUserId { get; set; }
        public IFormFile? Photo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
