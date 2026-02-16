using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Repository.Contexts.EfCore;

namespace BlogWebApplication.WebUI.Extensions
{
    public static class StartUpEx
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+*";
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 3;

            }).AddEntityFrameworkStores<AppDbContext>();
        }
        public static void AddCookieConfiguration(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder();
                cookieBuilder.Name = "BlogWebApplicationCookie";
                opt.LoginPath = new PathString("/Home/SignIn");
                opt.LogoutPath = new PathString("/Members/Logout");
                opt.Cookie = cookieBuilder;
                opt.ExpireTimeSpan = TimeSpan.FromDays(7);
                opt.SlidingExpiration = true;
            });
        }
    }
}
