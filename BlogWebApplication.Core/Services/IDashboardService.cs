namespace BlogWebApplication.Core.Services
{
    public interface IDashboardService
    {
        Task<List<int>> GetYearlyArticleCountAsync();
    }
}
