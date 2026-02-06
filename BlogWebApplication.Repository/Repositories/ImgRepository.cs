using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Repository.Contexts.EfCore;

namespace BlogWebApplication.Repository.Repositories
{
    public class ImgRepository : RepositoryBase<Img>, IImgRepository
    {
        public ImgRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
