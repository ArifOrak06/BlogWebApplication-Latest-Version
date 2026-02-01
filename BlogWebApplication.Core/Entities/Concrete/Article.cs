using BlogWebApplication.Core.Entities.Abstract;

namespace BlogWebApplication.Core.Entities.Concrete
{
    public class Article : BaseEntity, IEntity
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Img Img { get; set; }
        public Guid? ImgId { get; set; }
        public Category Category { get; set; }
        public Guid? CategoryId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid? AppUserId { get; set; }

    }
}
