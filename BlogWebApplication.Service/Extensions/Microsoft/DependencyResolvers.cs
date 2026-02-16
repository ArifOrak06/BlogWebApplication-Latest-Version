using BlogWebApplication.Core.Helpers;
using BlogWebApplication.Core.Services;
using BlogWebApplication.Service.Helpers;
using BlogWebApplication.Service.Services;
using BlogWebApplication.Service.Utilities.AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BlogWebApplication.Service.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void AddDependenciesForServiceLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ArticleProfile));
            services.AddScoped<IArticleService,ArticleService>();
            services.AddScoped<IAppUserService,AppUserService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IImgHelper, ImgHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
