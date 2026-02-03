using BlogWebApplication.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogWebApplication.Repository.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(5000);

            builder.HasOne(x => x.Img).WithMany(x => x.Articles).HasForeignKey(x => x.ImgId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Category).WithMany(x => x.Articles).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Articles).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Articles");
        }
    }
}
