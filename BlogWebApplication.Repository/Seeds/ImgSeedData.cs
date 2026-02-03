using BlogWebApplication.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogWebApplication.Repository.Seeds
{
    public class ImgSeedData : IEntityTypeConfiguration<Img>
    {
        public void Configure(EntityTypeBuilder<Img> builder)
        {
            builder.HasData(new Img[]
            {
                new()
                {
                    CreatedBy = "Admin Test",
                    CreatedDate = DateTime.Now,
                    Id = Guid.Parse("2771FF5B-9038-486A-901E-6B95F8F5EC75"),
                    FileName = "defaultUser.png",
                    FileType = "image/png",
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    
                }
            });
        }
    }
}
