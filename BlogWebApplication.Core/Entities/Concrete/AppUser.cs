using BlogWebApplication.Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace BlogWebApplication.Core.Entities.Concrete
{
    public class AppUser : IdentityUser<Guid>,IEntity
    {
        public ICollection<Article> Articles { get; set; }
    }
}
