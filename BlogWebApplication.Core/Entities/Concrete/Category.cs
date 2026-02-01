using BlogWebApplication.Core.Entities.Abstract;

namespace BlogWebApplication.Core.Entities.Concrete
{
    public class Category : BaseEntity,IEntity
    {
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
