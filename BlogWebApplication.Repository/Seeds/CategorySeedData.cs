using BlogWebApplication.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogWebApplication.Repository.Seeds
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category[]
            {
                new()
                {
                    Id = Guid.Parse("241D8C83-1AFD-487A-92B7-6E3FE9C2A5FE"),
                    Name = "Web Development",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin",

                }
            });
        }
    }
}
