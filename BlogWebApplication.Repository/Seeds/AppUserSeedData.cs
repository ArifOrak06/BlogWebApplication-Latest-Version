using BlogWebApplication.Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogWebApplication.Repository.Seeds
{
    public class AppUserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var superAdmin = new AppUser
            {
                Id = Guid.Parse("89884FBC-0E86-4A4B-BA2F-8A1AFE2B6B05"),
                UserName = "superadmin@blog.com",
                NormalizedUserName = "SUPERADMIN@BLOG.COM",
                Email = "superadmin@blog.com",
                NormalizedEmail = "SUPERADMIN@BLOG.COM",
                PhoneNumber = "1112222222",
                PhoneNumberConfirmed = true,
                ImgId = Guid.Parse("2771FF5B-9038-486A-901E-6B95F8F5EC75"),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "Password12*");

            builder.HasData(superAdmin);
        }
        private string CreatePasswordHash(AppUser user, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
