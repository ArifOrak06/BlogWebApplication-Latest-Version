using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Repository.Contexts.EfCore;
using BlogWebApplication.Repository.Extensions.Microsoft;
using BlogWebApplication.Service.Extensions.Microsoft;
using BlogWebApplication.Service.ValidationRules.ValidationRulesForArticleModels;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<ArticleCreateViewModelValidator>();
});


builder.Services.AddDependenciesForRepositoryLayer(builder.Configuration);
builder.Services.AddDependenciesForServiceLayer();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
//builder.Services.AddAutoMapper(typeof(ArticleProfile).Assembly);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
