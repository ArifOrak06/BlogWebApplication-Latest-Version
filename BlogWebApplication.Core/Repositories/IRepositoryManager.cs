namespace BlogWebApplication.Core.Repositories
{
    public interface IRepositoryManager
    {
        IArticleRepository ArticleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IImgRepository ImgRepository { get; }
    }
}
