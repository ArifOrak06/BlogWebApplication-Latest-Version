using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApplication.Service.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepositoryManager _repositoryManager;

        public DashboardService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<int>> GetYearlyArticleCountAsync()
        {

            // Özetle Yıllara göre makale analizi için tarih filtreli makale logici geliştirdik.

            var startDate = DateTime.Now.Date; // gün,ay,yıl, saat,dakika,saniye,salise bilgisine kadar döner
            startDate = new DateTime(startDate.Year, 1, 1);
            List<int> datas = new();
            for(int i = 1; i <= 12; i++)
            {
                var startedDate = new DateTime(startDate.Year, i, 1); // başlangıç tarihi ilgili (indise göre) ayın birinci günü
                var endedDate = startedDate.AddMonths(1); // bitiş tarihi ise yukarıda belirlenen başlangıç tarihine 1 ay ekleme yapıldığı haliyle belirlenen ayın birinci günü
                // belirlenen başlangıç ve bitiş tarihi aralığındaki tüm makale sayılarını tespit edip datas koleksiyonuna ekleyelim.
                var data = (await _repositoryManager.ArticleRepository.GetByFilter(false,x => x.IsActive&&x.CreatedDate >= startedDate &&x.CreatedDate <= endedDate).ToListAsync()).Count(); 
                datas.Add(data);
            }
            // tarih filtreli datayı dönüyoruz..
            return datas;

        }
    }
}
