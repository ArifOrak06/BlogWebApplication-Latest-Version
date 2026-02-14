using AutoMapper;
using BlogWebApplication.Core.Entities.Concrete;
using BlogWebApplication.Core.Models.CategoryModels;

namespace BlogWebApplication.Service.Utilities.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryViewModel>().ReverseMap();
            CreateMap<Category,CategoryCreateViewModel>().ReverseMap();
            CreateMap<Category,CategoryUpdateViewModel>().ReverseMap();

            CreateMap<CategoryViewModel, CategoryCreateViewModel>().ReverseMap();
            CreateMap<CategoryViewModel, CategoryUpdateViewModel>().ReverseMap();
            CreateMap<CategoryCreateViewModel, CategoryUpdateViewModel>().ReverseMap();
        }
    }
}
