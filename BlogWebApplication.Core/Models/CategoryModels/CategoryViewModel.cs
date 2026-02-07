using BlogWebApplication.Core.Models.ArticleModels;

namespace BlogWebApplication.Core.Models.CategoryModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ArticleViewModel> Articles { get; set; }

    }
}
