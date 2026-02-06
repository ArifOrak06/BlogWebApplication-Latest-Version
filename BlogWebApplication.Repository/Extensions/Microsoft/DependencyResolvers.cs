using BlogWebApplication.Core.Repositories;
using BlogWebApplication.Core.Utilities.Uow;
using BlogWebApplication.Repository.Contexts.EfCore;
using BlogWebApplication.Repository.Repositories;
using BlogWebApplication.Repository.Utilities.Uow;
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

            services.AddScoped(typeof(IRepositoryBase<>),typeof(RepositoryBase<>)); 
            services.AddScoped<IRepositoryManager,RepositoryManager>();
            services.AddScoped<IUow,Uow>();
        }
    }
}
