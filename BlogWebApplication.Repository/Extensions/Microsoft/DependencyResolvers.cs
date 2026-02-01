using BlogWebApplication.Repository.Contexts.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogWebApplication.Repository.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void AddDependenciesForRepositoryLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), opt =>
                {
                    opt.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name);
                }); 
            } );
        }
    }
}
