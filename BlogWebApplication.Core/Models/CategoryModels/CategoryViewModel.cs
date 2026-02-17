using BlogWebApplication.Core.Models.ArticleModels;

namespace BlogWebApplication.Core.Models.CategoryModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ArticleViewModel> Articles { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
