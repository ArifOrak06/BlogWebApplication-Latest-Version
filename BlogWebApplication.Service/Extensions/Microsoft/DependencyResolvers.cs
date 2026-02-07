using BlogWebApplication.Core.Services;
using BlogWebApplication.Service.Services;
using BlogWebApplication.Service.Utilities.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BlogWebApplication.Service.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void AddDependenciesForServiceLayer(this IServiceCollection services)
        {
       

            services.AddAutoMapper(typeof(ArticleProfile));
            services.AddScoped<IArticleService,ArticleService>();
        }
    }
}
