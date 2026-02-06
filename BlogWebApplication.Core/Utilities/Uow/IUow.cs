namespace BlogWebApplication.Core.Utilities.Uow
{
    public interface IUow
    {
        Task CommitAsync();
        void Commit();
    }
}
