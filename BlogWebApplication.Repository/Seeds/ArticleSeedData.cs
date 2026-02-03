using BlogWebApplication.Core.Entities.Abstract;
using BlogWebApplication.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogWebApplication.Repository.Seeds
{
    public class ArticleSeedData : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article[]
            {
                new()
                {
                    Id = Guid.Parse("DBBCBAA4-7800-4F5E-8595-E290225698D7"),
                    ImgId = Guid.Parse("2771FF5B-9038-486A-901E-6B95F8F5EC75"),
                    AppUserId = Guid.Parse("89884FBC-0E86-4A4B-BA2F-8A1AFE2B6B05"),
                    CategoryId = Guid.Parse("241D8C83-1AFD-487A-92B7-6E3FE9C2A5FE"),
                    Content = "Asp.Net Core 8.0 ile Muhasebe Programı Geliştirmek için,Asp.Net Core 8.0 ile Muhasebe Programı Geliştirmek için Asp.Net Core 8.0 ile Muhasebe Programı Geliştirmek için Asp.Net Core 8.0 ile Muhasebe Programı Geliştirmek için  ",
                    Title = "Asp.Net Core ile Web Geliştirme",
                    CreatedDate = DateTime.Now,
                    ModifiedDate =DateTime.Now,
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin",
                    IsActive =true,
                    IsDeleted=false,
                    
                }
            });
        }
    }
}
