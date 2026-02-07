using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.ArticleModels;

namespace BlogWebApplication.Service.Utilities.AutoMapper
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article,ArticleViewModel>().ReverseMap();
            CreateMap<Article,ArticleCreateViewModel>().ReverseMap();
            CreateMap<Article,ArticleUpdateViewModel>().ReverseMap();

            CreateMap<ArticleCreateViewModel, ArticleUpdateViewModel>().ReverseMap();
            CreateMap<ArticleViewModel, ArticleUpdateViewModel>().ReverseMap();
            CreateMap<ArticleViewModel, ArticleCreateViewModel>().ReverseMap();
        }
    }
}
