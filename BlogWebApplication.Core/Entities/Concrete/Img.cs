using BlogWebApplication.Core.Entities.Abstract;

namespace BlogWebApplication.Core.Entities.Concrete
{
    public class Img : BaseEntity,IEntity
    {
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
