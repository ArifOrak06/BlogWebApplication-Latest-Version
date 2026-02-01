using BlogWebApplication.Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace BlogWebApplication.Core.Entities.Concrete
{
    public class AppRole: IdentityRole<Guid>, IEntity
    {
    }
}
